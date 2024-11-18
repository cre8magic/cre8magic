using System.Text.Json.Serialization;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Services.Internal;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using static System.StringComparer;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// All the current "global" settings of a page, which apply to anything on the page.
/// </summary>
public record MagicAllSettings: IHasSettingsExceptions, IHasDebugSettings, ICanClone<MagicAllSettings>
{
    internal MagicAllSettings(string name, IMagicSettingsService service, MagicThemeSettings themeSettings, TokenEngine tokens, PageState pageState)
    {
        Name = name;
        Service = service;
        ThemeSettings = themeSettings;
        Tokens = tokens;
        PageState = pageState;
    }

    internal MagicAllSettings(MagicAllSettings? priority, MagicAllSettings? fallback = default)
    {
        PageState = priority?.PageState ?? fallback?.PageState ?? throw new("PageState is required");
        Tokens = priority?.Tokens ?? fallback?.Tokens ?? throw new("Tokens are required");
        MagicContext = priority?.MagicContext ?? fallback?.MagicContext ?? "";
        Name = priority?.Name ?? fallback?.Name ?? "unknown";


        Service = priority?.Service ?? fallback?.Service ?? throw new("Service is required");
        ThemeSettings = priority?.ThemeSettings ?? fallback?.ThemeSettings ?? throw new("Theme is required");
    }

    public MagicAllSettings CloneWith(MagicAllSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    public MagicDebugState Debug => _debug ??= DebugState(ThemeSettings);
    private MagicDebugState? _debug;

    /// <summary>
    /// This is only used to detect if debugging should be active, and the setting should come from the theme itself
    /// </summary>
    MagicDebugSettings? IHasDebugSettings.Debug => ThemeSettings.Debug;


    public MagicDebugState DebugState(object? target) => Service.Debug.GetState(target, PageState.UserIsAdmin());

    internal PageState PageState { get; }

    internal TokenEngine Tokens { get; }

    public string MagicContext { get; set; } = "";

    public string Name { get; }

    [JsonIgnore]
    public IMagicSettingsService Service { get; }
    [JsonIgnore]
    internal MagicThemeDesigner ThemeDesigner => _themeDesigner ??= new(new MagicThemeContext(Name, PageState, ThemeSettings, ThemeDesignSettings, Tokens, []));
    private MagicThemeDesigner? _themeDesigner;

    public MagicThemeSettings ThemeSettings { get; }

    /// <summary>
    /// Determine if we should show a specific part
    /// </summary>
    public bool Show(string name) =>
        ThemeSettings.Parts.TryGetValue(name, out var value) && value.Show == true;

    public MagicAnalyticsSettings Analytics =>
        _a ??= Service.Analytics.Find(ThemeSettings.Parts.GetThemePartRenameOrFallback(nameof(Analytics), Name), Name);
    private MagicAnalyticsSettings? _a;

    public MagicThemeDesignSettings ThemeDesignSettings =>
        _td ??= ((MagicSettingsService)Service).ThemeDesignSettings(ThemeSettings, Name);
    //_td ??= Service.ThemeDesign.Find(ThemeSettings.Design ?? ThemeSettings.Parts.GetThemePartRenameOrFallback(nameof(ThemeSettings.Design), Name), Name);
    private MagicThemeDesignSettings? _td;

    public MagicLanguageSettings Languages =>
        _l ??= Service.Languages.Find(ThemeSettings.Parts.GetThemePartRenameOrFallback(nameof(Languages), Name), Name);
    private MagicLanguageSettings? _l;

    public Dictionary<string, string> DebugSources { get; } = new(InvariantCultureIgnoreCase);

    public List<Exception> Exceptions => Service.Exceptions;

}