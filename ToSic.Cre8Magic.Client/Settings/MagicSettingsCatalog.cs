using System.Text.Json.Serialization;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Settings.Internal.Logging;
using ToSic.Cre8magic.Themes.Settings;
using static System.StringComparer;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// This is a catalog of all kinds of configurations.
/// It serves as a kind of database to manage all configurations, which will usually be retrieved using a name. 
/// </summary>
public record MagicSettingsCatalog: IHasDebugSettings
{
    public MagicSettingsCatalog() { }


    public const string SourceDefault = "Unknown";
    /// <summary>
    /// Version number when loading from JSON to verify it's what we expect
    /// </summary>
    public float Version { get; init; }

    /// <summary>
    /// Master debug settings - would override other debugs
    /// </summary>
    public MagicDebugSettings? Debug { get; init; } = new();

    /// <summary>
    /// Source of these settings / where they came from, to ensure that we can see in debug where a value was picked up from
    /// </summary>
    public string Source { get; set; } = SourceDefault;

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicAnalyticsSettings>))]
    public Dictionary<string, MagicAnalyticsSettings> Analytics { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBreadcrumbSettings>))]
    public Dictionary<string, MagicBreadcrumbSettings> Breadcrumbs { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicThemeSettings>))]
    public Dictionary<string, MagicThemeSettings> Themes { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicThemeDesignSettings>))]
    public Dictionary<string, MagicThemeDesignSettings> ThemeDesigns { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicContainerSettings>))]
    public Dictionary<string, MagicContainerSettings> Containers { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicLanguageSettings>))]
    public Dictionary<string, MagicLanguageSettings> Languages { get; init; } = new(InvariantCultureIgnoreCase);

    /// <summary>
    /// The menu definitions
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicMenuSettingsData>))]
    public Dictionary<string, MagicMenuSettingsData> Menus { get; init; } = new(InvariantCultureIgnoreCase);

    /// <summary>
    /// Design definitions of the menu
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<Dictionary<string, MagicMenuDesignSettings>>))]
    public Dictionary<string, Dictionary<string, MagicMenuDesignSettings>> MenuDesigns { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonIgnore]
    internal SettingsLogs Logs { get; init; } = new(null);

    internal static MagicSettingsCatalog Fallback = new()
    {
        Version = -1.0f,
        Source = "Fallback",
    };
}