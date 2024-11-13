using System.Text.Json;
using Microsoft.Extensions.Logging;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Json;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="IMagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicPackageSettings"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
public class MagicSettingsLoader(MagicSettingsJsonService jsonService, ILogger<MagicSettingsLoader> logger)
    : IHasSettingsExceptions
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
        var priority = JsonSerializer.Serialize(sources.First());
        foreach (var source in sources.Skip(1))
        {
            // get new json
            var lowerPriority = JsonSerializer.Serialize(source, JsonMerger.GetNewOptionsForPreMerge(logger));
            var merged = JsonMerger.Merge(priority, lowerPriority);
            priority = merged;
        }
        var result = JsonSerializer.Deserialize<MagicSettingsCatalog>(priority);
        return result!;
    }

    private List<MagicSettingsCatalog> ConfigurationSources => _configurationSources ??= Load();
    private List<MagicSettingsCatalog>? _configurationSources;

    private List<MagicSettingsCatalog> Load()
    {
        if (string.IsNullOrWhiteSpace(PackageSettings.SettingsJsonFile))
            return PackageSettings.Defaults == null
                ? []
                : [PackageSettings.Defaults];

        var catalogFromJson = jsonService.LoadJson(PackageSettings);
        var sources = new List<MagicSettingsCatalog?>
            {
                // 1. in future also add the settings from the dialog as the first priority
                // 2. then the settings from the json file
                catalogFromJson,
                // 3. fallback which can be specified by the theme
                PackageSettings.Defaults,
            }
            .Where(x => x != null)
            .Cast<MagicSettingsCatalog>()
            .ToList();
        return sources;
    }

    public List<Exception> Exceptions => MyExceptions.Concat(jsonService.Exceptions).ToList();
    private List<SettingsException> MyExceptions { get; } = [];

}