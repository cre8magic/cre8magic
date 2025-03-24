using System.Collections;
using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Utils;

using static ToSic.Cre8magic.MagicConstants;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Special helper to do a few things with settings.
///
/// 1. It receives a list of Books which it will scan
/// 2. It also receives a function to get only the part of the spells book it's interested in
///    ...this is to get type safety and everything, like it will only look at the Analytics settings.
/// </summary>
/// <typeparam name="TSettingsData"></typeparam>
/// <param name="hasLibrary"></param>
/// <param name="defaults"></param>
/// <param name="getSection"></param>
internal class SettingsReader<TSettingsData>(
    IHasMagicLibrary hasLibrary,
    Defaults<TSettingsData> defaults,
    Func<MagicBook, IDictionary<string, TSettingsData>> getSection
)
    where TSettingsData : class, new()
{
    /// <summary>
    /// Create a clone of the settings reader, which will specifically only use test books provided by the source.
    /// </summary>
    internal SettingsReader<TSettingsData> MaybeUseCustomBook(MagicBook? book)
        => book == null ? this : new(new HasMagicLibrary([new(book, new())]), defaults, getSection);

    /// <summary>
    /// Find the settings according to the names, and (if not null) merge with priority.
    /// </summary>
    internal DataWithJournal<TSettingsData> FindAndMerge(FindSettingsNameSpecs nameSpecs, TSettingsData? priority = null)
    {
        var name = nameSpecs.Name;
        Journal journal;
        if (name.IsNullOrEmpty())
            (name, journal) = nameSpecs.ThemeContext.NameResolver.FindBestNameAccordingToParts(nameSpecs);
        else
            journal = new();

        var found = FindAndNeutralize(name!);
        var part = MergeHelper.TryToMergeOrKeepPriority(priority, found);

        return new(part, journal);
    }

    /// <summary>
    /// Find a part by name, and merge it with the foundation if applicable.
    /// This is to ensure necessary basics are always present, even if the part doesn't specify them.
    /// </summary>
    /// <returns></returns>
    internal TSettingsData FindAndNeutralize(string name)
    {
        // Create array of names to look up, the first one is the main name (specify type so clearly non-null)
        var mainName = name.IsNullOrEmpty() ? Default : name;

        // Get best matching part; returns null if nothing found
        var priority = FindInSourcesOrNull(mainName);
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

        return mergedNew;

        // Inner function to find settings and merge them
        TSettingsData FindSettingsAndTryMerge(TSettingsData priorityData, string nameToFind)
        {
            var addition = FindInSourcesOrNull(nameToFind);
            return addition == null
                ? priorityData
                : MergeHelper.TryToMergeOrKeepPriority(priorityData, addition)!;
        }
    }


    /// <summary>
    /// Find the settings in all possible sources.
    /// </summary>
    /// <returns></returns>
    private TSettingsData? FindInSourcesOrNull(string name)
    {
        // Make sure we have at least one name
        name = name.IsNullOrEmpty() ? Default : name;

        // Get all spells-books (e.g. provided by code in theme, from JSON, etc.)
        var books = hasLibrary.Library;

        // Create a list of all possible sources and names
        // Prioritize the names, and then go through all sources for each name
        var allSourcesAndNames = books
            .Select(book => (Book: book.Data, name))
            .ToList();

        foreach (var set in allSourcesAndNames)
        {
            // Get the section using the helper which was given to this object
            var section = getSection(set.Book);

            // Check if we can find a setting in this section and return
            if (section.TryGetValue(set.name, out var settings))
                return settings;
        }

        return null;
    }


}