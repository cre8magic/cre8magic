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
    Func<MagicSettingsCatalog, NamedSettings<TPart>> findList,
    //Func<List<NamedSettings<TPart>>>? findAllSources = default,
    bool useAllSources = false,
    Func<string, TPart, TPart>? modify = default)
    where TPart : class, ICanClone<TPart>, new()
{
    internal TPart Find(string name, string? defaultName = null)
    {
        var names = GetConfigNamesToCheck(name, defaultName ?? name);
        var realName = names[0];
        var cached = _cache.FindInvariant(realName);
        if (cached != null)
            return cached;

        // Get best part; return Fallback if nothing found
        var priority = FindPart(names);
        if (priority == null)
            return defaults.Fallback;

        // Check if our part declares that it inherits something
        if (priority is SettingsWithInherit couldInherit && couldInherit.Inherits.HasText())
        {
            // Remember inherits-from setting, and then remove from the part
            var inheritFrom = couldInherit.Inherits;
            couldInherit = couldInherit with { Inherits = null };
            priority = couldInherit as TPart ?? priority;

            priority = FindPartAndMergeIfPossible(priority, realName, inheritFrom);
        }
        else if (priority is NamedSettings<MagicMenuDesignSettings> priorityNamed 
                 && priorityNamed.TryGetValue(InheritsNameInJson, out var value))
        {
            priorityNamed.Remove(InheritsNameInJson);
            if (value.Value != null) priority = FindPartAndMergeIfPossible(priority, realName, value.Value);
        }

        if (defaults.Foundation == null)
            return priority;

        var mergedNew = defaults.Foundation.CloneWith(priority);
        if (modify != null)
            mergedNew = modify(realName, mergedNew);

        _cache[realName] = mergedNew;
        return mergedNew!;
    }

    private TPart FindPartAndMergeIfPossible(TPart priority, string realName, string name)
    {
        var addition = FindPart(name);
        if (addition == null)
            return priority;
        var mergeNew = addition.CloneWith(priority);
        if (modify != null)
            mergeNew = modify(realName, mergeNew);

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

        var catalogs = useAllSources
            ? settingsSvc.AllCatalogs
            : [settingsSvc.Catalog];

        var allSourcesAndNames = names
            .Distinct()
            .SelectMany(name => catalogs.Select(catalog => (catalog, name)))
            .ToList();

        foreach (var set in allSourcesAndNames)
        {
            var result = findList(set.catalog).GetInvariant(set.name);
            if (result != null) return result;
        }

        return default;
    }


}