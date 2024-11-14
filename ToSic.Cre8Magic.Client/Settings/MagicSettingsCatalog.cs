using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Languages.Settings;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Debug;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// This is a catalog of all kinds of configurations.
/// It serves as a kind of database to manage all configurations, which will usually be retrieved using a name. 
/// </summary>
public record MagicSettingsCatalog: IHasDebugSettings
{
    public MagicSettingsCatalog() { }

    public MagicSettingsCatalog(MagicSettingsCatalog? priority, MagicSettingsCatalog? fallback = default)
    {
        Version = priority?.Version ?? fallback?.Version ?? Fallback.Version;
        Debug = priority?.Debug ?? fallback?.Debug;
        Source = priority?.Source ?? fallback?.Source ?? Fallback.Source;

        Analytics = new(priority?.Analytics, fallback?.Analytics);
        Themes = new(priority?.Themes, fallback?.Themes);
        ThemeDesigns = new(priority?.ThemeDesigns, fallback?.ThemeDesigns);
        Containers = new(priority?.Containers, fallback?.Containers);
        Languages = new(priority?.Languages, fallback?.Languages);
        Menus = new(priority?.Menus, fallback?.Menus);
        MenuDesigns = new(priority?.MenuDesigns, fallback?.MenuDesigns);
    }

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

    public NamedSettings<MagicAnalyticsSettings> Analytics { get; init; } = new();

    public NamedSettings<MagicThemeSettings> Themes { get; init; } = new();

    public NamedSettings<MagicThemeDesignSettings> ThemeDesigns { get; init; } = new();

    public NamedSettings<MagicContainerSettings> Containers { get; init; } = new();

    public NamedSettings<MagicLanguagesSettings> Languages { get; init; } = new();

    /// <summary>
    /// The menu definitions
    /// </summary>
    public NamedSettings<MagicMenuSettings> Menus { get; init; } = new();

    /// <summary>
    /// Design definitions of the menu
    /// </summary>
    public NamedSettings<NamedSettings<MagicMenuDesignSettings>> MenuDesigns { get; init; } = new();

    internal static MagicSettingsCatalog Fallback = new()
    {
        Version = -1.0f,
        Source = "Fallback",
    };
}