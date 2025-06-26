using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="IMagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicThemePackage"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
public class MagicLibraryLoader(IEnumerable<IMagicBooksSource> sources)
{
    public List<DataWithJournal<MagicBook>> Books(MagicThemePackage themePackage, bool cache = true) => 
        cache ? _cache ??= Load(themePackage) : Load(themePackage);
    private List<DataWithJournal<MagicBook>>? _cache;

    private List<DataWithJournal<MagicBook>> Load(MagicThemePackage themePackage)
    {
        // Typical sources
        // 100 Package Settings JSON
        // -100 Package Defaults
        var sources2 = sources
            .OrderByDescending(s => s.Priority)
            .SelectMany(s => s.Books(themePackage))
            .ToList();
;
        return sources2;
    }

}