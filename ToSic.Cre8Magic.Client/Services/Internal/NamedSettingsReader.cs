using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.Client.MagicConstants;
using static ToSic.Cre8magic.Settings.SettingsWithInherit;

namespace ToSic.Cre8magic.Services.Internal;

internal class NamedSettingsReader<TPart>(
    IMagicSettingsService settingsSvc,
    Defaults<TPart> defaults,
    Func<MagicSettingsCatalog, NamedSettings<TPart>> findList
)
    where TPart : class, ICanClone<TPart>, new()
{

    /// <summary>
    /// Find the settings according to the names, and (if not null) merge with priority.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultName"></param>
    /// <param name="priority"></param>
    /// <param name="skipCache"></param>
    /// <returns></returns>
    internal TPart FindAndMerge(string name, string? defaultName = null, TPart? priority = null, bool skipCache = false)
    {
        var found = FindAndNeutralize(name, defaultName, skipCache);
        return priority == null
            ? found
            : found.CloneUnder(priority);
    }

    /// <summary>
    /// Find a part by name, and merge it with the foundation if applicable.
    /// This is to ensure necessary basics are always present, even if the part doesn't specify them.
    /// </summary>
    /// <param name="name">The name to look for.</param>
    /// <param name="defaultName">Name of the current theme settings, which is used for fallback options.</param>
    /// <param name="skipCache"></param>
    /// <returns></returns>
    internal TPart FindAndNeutralize(string name, string? defaultName = null, bool skipCache = false)
    {
        // Create array of names to look up, the first one is the main name
        var names = GetConfigNamesToCheck(name, defaultName ?? name);
        var mainName = names[0];

        // Check cache if applicable
        if (!skipCache && _cache.TryGetValue(mainName, out var cached2))
            return cached2;

        // Get best matching part; returns null if nothing found
        var priority = FindPart(names);
        switch (priority)
        {
            // Nothing found, return fallback
            case null:
                return defaults.Fallback;

            // Check if our part declares that it inherits something
            case SettingsWithInherit couldInherit when couldInherit.Inherits.HasText():
                // Remember inherits-from setting, and then remove from the part
                var inheritFrom = couldInherit.Inherits;
                priority = couldInherit with { Inherits = null } as TPart ?? priority;
                priority = FindPartAndMergeIfPossible(priority, mainName, inheritFrom);
                break;

            // Check if it's a dictionary containing @inherit specs
            case NamedSettings<MagicMenuDesignSettings> named when named.TryGetValue(InheritsNameInJson, out var value):
                if (value.Value != null)
                    priority = FindPartAndMergeIfPossible(priority, mainName, value.Value);
                else
                    named.Remove(InheritsNameInJson);
                break;
        }

        // If we don't have a foundation to mix in, we're done
        if (defaults.Foundation == null)
            return priority;

        var mergedNew = defaults.Foundation.CloneUnder(priority);

        if (!skipCache)
            _cache[mainName] = mergedNew;
        return mergedNew;
    }

    private TPart FindPartAndMergeIfPossible(TPart priority, string realName, string name)
    {
        var addition = FindPart(name);
        if (addition == null)
            return priority;
        var mergeNew = addition.CloneUnder(priority);

        return mergeNew;
    }

    private readonly NamedSettings<TPart> _cache = new();

    private static string[] GetConfigNamesToCheck(string? configuredNameOrNull, string currentName)
    {
        if (configuredNameOrNull == InheritName)
            configuredNameOrNull = currentName;

        return configuredNameOrNull.HasText()
            ? new[] { configuredNameOrNull, Default }.Distinct().ToArray()
            : [Default];
    }

    internal TPart? FindPart(params string[]? names)
    {
        // Make sure we have at least one name
        if (names == null || names.Length == 0) names = [Default];

        // #WipRemovingPreMergedCatalog
        var catalogs = // useAllSources || true
            /*?*/ settingsSvc.AllCatalogs
            //: [settingsSvc.Catalog]
            ;

        var allSourcesAndNames = names
            .Distinct()
            .SelectMany(name => catalogs.Select(catalog => (catalog, name)))
            .ToList();

        foreach (var set in allSourcesAndNames)
            if (findList(set.catalog).TryGetValue(set.name, out var result) && result != null)
                return result;
        //var result = findList(set.catalog).GetInvariant(set.name);
        //if (result != null) return result;

        return null;
    }


}