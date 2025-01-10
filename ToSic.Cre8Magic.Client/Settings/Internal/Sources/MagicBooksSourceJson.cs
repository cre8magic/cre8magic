using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Json;

namespace ToSic.Cre8magic.Settings.Internal.Sources;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="IMagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicThemePackage"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
internal class MagicBooksSourceJson(MagicBookLoaderJson bookLoaderJson) : IMagicBooksSource
{
    public int Priority => 100;

    public List<DataWithJournal<MagicBook>> Books(MagicThemePackage themePackage)
    {
        if (themePackage == null)
            throw new ArgumentNullException(nameof(themePackage));

        if (_cache.TryGetValue(themePackage, out var cached))
            return cached;

        if (string.IsNullOrWhiteSpace(themePackage.SettingsJsonFile))
            return [];

        var spellsBook = bookLoaderJson.LoadJson(themePackage);

        var bundle = new List<DataWithJournal<MagicBook>> { spellsBook };
        _cache[themePackage] = bundle;
        return bundle;
    }

    /// <summary>
    /// Note: don't make static, otherwise we can't see json-file changes
    /// </summary>
    private readonly Dictionary<MagicThemePackage, List<DataWithJournal<MagicBook>>> _cache = new();
}