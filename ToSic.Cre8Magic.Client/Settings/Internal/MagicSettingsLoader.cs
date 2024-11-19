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
    public MagicSettingsLoader Setup(MagicPackageSettings packageSettings)
    {
        _packageSettings = packageSettings;
        return this;
    }

    /// <summary>
    /// Safe access to Package Settings, with warning if not setup correctly.
    /// </summary>
    internal MagicPackageSettings PackageSettings => _packageSettings ?? MagicPackageSettings.Fallback;
    private MagicPackageSettings? _packageSettings;


    public MagicDebugSettings? DebugSettings => _debug ??= ConfigurationSources.FirstOrDefault(c => c.Debug != null)?.Debug;
    private MagicDebugSettings? _debug;

    internal MagicSettingsCatalog MergeCatalogs()
    {
        var sources = ConfigurationSources;

        if (sources.Count == 0)
            return new();
        var prioCat = sources.First();
        if (sources.Count == 1)
            return prioCat;

        prioCat = sources.Aggregate(prioCat, (current, catalog) => new(current, catalog));

        return prioCat;
    }

    private List<MagicSettingsCatalog> ConfigurationSources => _configurationSources ??= Load();
    private List<MagicSettingsCatalog>? _configurationSources;

    private List<MagicSettingsCatalog> Load()
    {
        // Typical sources
        // 100 Package Settings JSON
        // -100 Package Defaults
        var sources2 = sources
            .OrderByDescending(s => s.Priority)
            .Select(s => s.Get(PackageSettings))
            .Where(c => c?.Catalog != null)
            .ToList();

        // Merge & keep all exceptions
        Exceptions = sources2.SelectMany(c => c!.Exceptions ?? []).ToList();

        return sources2.Select(c => c!.Catalog!).ToList();

    }

    internal List<Exception> Exceptions { get; set; } = [];

}