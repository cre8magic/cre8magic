using System.Text.Json.Serialization;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using static System.StringComparer;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// All the current "global" settings of a page, which apply to anything on the page.
/// </summary>
public record MagicAllSettings: IHasSettingsExceptions, IHasDebugSettings, ICanClone<MagicAllSettings>
{
    internal MagicAllSettings(string name, IMagicSettingsService service, MagicThemeSettings theme, TokenEngine tokens, PageState pageState)
    {
        Name = name;
        Service = service;
        Theme = theme;
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
        Theme = priority?.Theme ?? fallback?.Theme ?? throw new("Theme is required");
    }

    public MagicAllSettings CloneWith(MagicAllSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    public MagicDebugState Debug => _debug ??= DebugState(Theme);
    private MagicDebugState? _debug;

    /// <summary>
    /// This is only used to detect if debugging should be active, and the setting should come from the theme itself
    /// </summary>
    MagicDebugSettings? IHasDebugSettings.Debug => Theme.Debug;


    public MagicDebugState DebugState(object? target) => Service.Debug.GetState(target, PageState.UserIsAdmin());

    internal PageState PageState { get; }

    internal TokenEngine Tokens { get; }

    public string MagicContext { get; set; } = "";

    public string Name { get; }

    [JsonIgnore]
    public IMagicSettingsService Service { get; }
    [JsonIgnore]
    internal ThemeDesigner ThemeDesigner => _themeDesigner ??= new(this);
    private ThemeDesigner? _themeDesigner;

    public MagicThemeSettings Theme { get; }

    /// <summary>
    /// Determine if we should show a specific part
    /// </summary>
    public bool Show(string name) =>
        Theme.Parts.TryGetValue(name, out var value) && value.Show == true;

    /// <summary>
    /// Determine the name of the design configuration of a specific part
    /// </summary>
    internal string? DesignName(string name) =>
        Theme.Parts.TryGetValue(name, out var value)
            ? value.Design
            : null;

    /// <summary>
    /// Determine the configuration name of a specific part.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    internal string? GetThemePartRenameOrNull(string name) =>
        Theme.Parts.TryGetValue(name, out var value)
            ? value.Configuration
            : null;

    internal string GetThemePartRenameOrDefault(string name) =>
        GetThemePartRenameOrNull(name) ?? Name;

    public MagicAnalyticsSettings Analytics =>
        _a ??= Service.Analytics.Find(GetThemePartRenameOrDefault(nameof(Analytics)), Name);
    private MagicAnalyticsSettings? _a;

    public MagicThemeDesignSettings ThemeDesign =>
        _td ??= Service.ThemeDesign.Find(Theme.Design ?? GetThemePartRenameOrDefault(nameof(Theme.Design)), Name);
    private MagicThemeDesignSettings? _td;

    public MagicLanguageSettings Languages =>
        _l ??= Service.Languages.Find(GetThemePartRenameOrDefault(nameof(Languages)), Name);
    private MagicLanguageSettings? _l;

    public Dictionary<string, string> DebugSources { get; } = new(InvariantCultureIgnoreCase);

    public List<Exception> Exceptions => Service.Exceptions;

}