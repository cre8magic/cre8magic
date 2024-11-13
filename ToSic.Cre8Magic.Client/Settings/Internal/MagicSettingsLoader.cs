using System.Text.Json;
using Microsoft.Extensions.Logging;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Json;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="MagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicPackageSettings"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
public class MagicSettingsLoader(MagicSettingsJsonService jsonService, ILogger<MagicSettingsService> logger)
    : IHasSettingsExceptions
{
    public MagicSettingsLoader Setup(MagicPackageSettings packageSettings)
    {
        PackageSettings = packageSettings;
        return this;
    }

    /// <summary>
    /// Safe access to Package Settings, with warning if not setup correctly.
    /// </summary>
    internal MagicPackageSettings PackageSettings
    {
        get => _packageSettings ?? throw new ArgumentException($"You must first call {nameof(Setup)}", nameof(PackageSettings));
        set => _packageSettings = value;
    }
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
            var sources = new List<MagicSettingsCatalog?>
                {
                    // in future also add the settings from the dialog as the first priority
                    jsonService.LoadJson(PackageSettings),
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