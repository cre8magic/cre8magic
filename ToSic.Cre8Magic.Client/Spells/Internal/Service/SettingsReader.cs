using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Docs;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.MagicConstants;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Special helper to do a few things with settings.
///
/// 1. It receives a list of SpellsBooks which it will scan
/// 2. It also receives a function to get only the part of the spells book it's interested in
///    ...this is to get type safety and everything, like it will only look at the Analytics settings.
/// </summary>
/// <typeparam name="TSettingsData"></typeparam>
/// <param name="hasSpellsLibrary"></param>
/// <param name="defaults"></param>
/// <param name="getSection"></param>
internal class SettingsReader<TSettingsData>(
    IHasSpellsLibrary hasSpellsLibrary,
    Defaults<TSettingsData> defaults,
    Func<MagicSpellsBook, IDictionary<string, TSettingsData>> getSection
)
    where TSettingsData : class, new()
{
    /// <summary>
    /// Create a clone of the settings reader, which will specifically only use test books provided by the source.
    /// </summary>
    internal SettingsReader<TSettingsData> MaybeUseCustomSpellsBook(MagicSpellsBook? book)
        => book == null ? this : new(new HasSpellsLibrary([new(book, new())]), defaults, getSection);

    /// <summary>
    /// Find the settings according to the names, and (if not null) merge with priority.
    /// </summary>
    internal DataWithJournal<TSettingsData> FindAndMerge(FindSettingsSpecs specs, TSettingsData? priority = null, bool skipCache = true)
    {
        var (bestPartName, journal) = specs.Context.NameResolver.FindBestNameAccordingToParts(specs);

        // var settingsName
        var found = FindAndNeutralize([specs.SettingsName, bestPartName, specs.ThemeName], skipCache: skipCache);
        var part = MergeHelper.TryToMergeOrKeepPriority(priority, found)!;

        return new(part, journal);
    }

    /// <summary>
    /// Find a part by name, and merge it with the foundation if applicable.
    /// This is to ensure necessary basics are always present, even if the part doesn't specify them.
    /// </summary>
    /// <param name="names"></param>
    /// <param name="skipCache"></param>
    /// <returns></returns>
    internal TSettingsData FindAndNeutralize(string?[] names, bool skipCache = true)
    {
        // Create array of names to look up, the first one is the main name (specify type so clearly non-null)
        var cleanNames = ((string[])[ ..names, Default ])
            .Where(s => s.HasText())
            .Select(s => s!.Trim())
            .Distinct()
            .ToArray()!;

        var mainName = cleanNames[0];

        // Check cache if applicable
        if (!skipCache && _cache.TryGetValue(mainName, out var cached2))
            return cached2;

        // Get best matching part; returns null if nothing found
        var priority = FindSettingsData(cleanNames);
        switch (priority)
        {
            // Nothing found, return fallback
            case null:
                return defaults.Fallback;

            // Check if our part declares that it inherits something
            case MagicInheritsBase couldInherit when couldInherit.Inherits.HasText():
                // Remember inherits-from setting, and then remove from the part
                var inheritFrom = couldInherit.Inherits;
                priority = couldInherit with { Inherits = null } as TSettingsData ?? priority;
                priority = FindSettingsAndTryMerge(priority, inheritFrom);
                break;
        }

        // If we don't have a foundation to mix in, we're done
        if (defaults.Foundation == null)
            return priority;

        var mergedNew = MergeHelper.TryToMergeOrKeepPriority(priority, defaults.Foundation)!;

        if (!skipCache)
            _cache[mainName] = mergedNew;
        return mergedNew;

        // Inner function to find settings and merge them
        TSettingsData FindSettingsAndTryMerge(TSettingsData priorityData, string nameToFind)
        {
            var addition = FindSettingsData([nameToFind]);
            return addition == null
                ? priorityData
                : MergeHelper.TryToMergeOrKeepPriority(priorityData, addition)!;
        }
    }

    private readonly Dictionary<string, TSettingsData> _cache = new(StringComparer.InvariantCultureIgnoreCase);

    private TSettingsData? FindSettingsData(string[]? names)
    {
        // Make sure we have at least one name
        if (names == null || names.Length == 0) names = [Default];

        // Get all spells-books (e.g. provided by code in theme, from JSON, etc.)
        var books = hasSpellsLibrary.Library;

        // Create a list of all possible sources and names
        // Prioritize the names, and then go through all sources for each name
        var allSourcesAndNames = names
            .Distinct()
            .SelectMany(name => books.Select(book => (Book: book.Data, name)))
            .ToList();

        foreach (var set in allSourcesAndNames)
        {
            var section = getSection(set.Book);
            if (section.TryGetValue(set.name, out var settings))
                return settings;
        }

        return null;
    }


}