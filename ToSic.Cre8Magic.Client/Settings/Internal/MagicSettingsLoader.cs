using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="IMagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicPackageSettings"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
public class MagicSettingsLoader(IEnumerable<IMagicSettingsSource> sources)
{
    internal MagicSettingsCatalog MergeCatalogs(MagicPackageSettings packageSettings)
    {
        var sources = (_catalogs ??= Load(packageSettings));

        if (sources.Count == 0)
            return new();
        var prioCat = sources.First();
        if (sources.Count == 1)
            return prioCat;

        prioCat = sources.Aggregate(prioCat, (current, catalog) => new(current, catalog));

        return prioCat;
    }

    //private List<MagicSettingsCatalog> GetCatalogs(MagicPackageSettings? packageSettings) => _catalogs ??= Load(packageSettings);
    private List<MagicSettingsCatalog>? _catalogs;

    private List<MagicSettingsCatalog> Load(MagicPackageSettings packageSettings)
    {
        // Typical sources
        // 100 Package Settings JSON
        // -100 Package Defaults
        var sources2 = sources
            .OrderByDescending(s => s.Priority)
            .Select(s => s.Get(packageSettings))
            .Where(c => c?.Catalog != null)
            .ToList();

        // Merge & keep all exceptions
        Exceptions = sources2.SelectMany(c => c!.Exceptions ?? []).ToList();

        return sources2.Select(c => c!.Catalog!).ToList();

    }

    internal List<Exception> Exceptions { get; set; } = [];

}