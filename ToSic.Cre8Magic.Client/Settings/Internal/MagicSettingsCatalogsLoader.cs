using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="IMagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicPackageSettings"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
public class MagicSettingsCatalogsLoader(IEnumerable<IMagicSettingsSource> sources)
{
    public List<DataWithJournal<MagicSettingsCatalog>> Catalogs(MagicPackageSettings packageSettings, bool cache = true) => 
        cache ? _catalogs ??= Load(packageSettings) : Load(packageSettings);
    private List<DataWithJournal<MagicSettingsCatalog>>? _catalogs;

    private List<DataWithJournal<MagicSettingsCatalog>> Load(MagicPackageSettings packageSettings)
    {
        // Typical sources
        // 100 Package Settings JSON
        // -100 Package Defaults
        var sources2 = sources
            .OrderByDescending(s => s.Priority)
            .SelectMany(s => s.Catalogs(packageSettings))
            .ToList();
;
        return sources2;
    }

}