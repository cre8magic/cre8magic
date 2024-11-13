using Microsoft.Extensions.Logging;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Languages.Settings;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Services.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.Client.MagicConstants;

namespace ToSic.Cre8magic.Client.Services;

/// <summary>
/// Service which consolidates settings made in the UI, in the JSON and falls back to coded defaults.
/// </summary>
internal class MagicSettingsService(ILogger<IMagicSettingsService> logger, MagicSettingsLoader loader)
    : IMagicSettingsService
{
    public IMagicSettingsService Setup(MagicPackageSettings packageSettings)
    {
        _packageSettings = packageSettings;
        loader.Setup(packageSettings);
        return this;
    }

    MagicDebugSettings IMagicSettingsService.Debug => _debug ??= loader.DebugSettings ?? MagicDebugSettings.Defaults.Fallback;
    private MagicDebugSettings? _debug;

    private MagicPackageSettings PackageSettings => _packageSettings ?? MagicPackageSettings.Fallback;
    private MagicPackageSettings? _packageSettings;

    /// <summary>
    /// Logger, provided to the <see cref="NamedSettingsReader{TPart}"/>
    /// </summary>
    ILogger<IMagicSettingsService> IMagicSettingsService.Logger { get; } = logger;

    public MagicAllSettings CurrentSettings(PageState pageState, string? name, string bodyClasses)
    {
        // Get a cache-id for this specific configuration, which can vary by page
        var originalNameForCache = (name ?? "prevent-error") + pageState.Page.PageId;
        var cached = _currentSettingsCache.FindInvariant(originalNameForCache);
        if (cached != null) return cached;

        // Tokens engine for this specific PageState
        var pageFactory = new MagicPageFactory(pageState);
        var tokens = new TokenEngine([
            new PageTokens(pageFactory.Current, null, bodyClasses),
            new ThemeTokens(PackageSettings)
        ]);

        // Figure out real config-name, and get the initial layout
        var configDetails = FindConfigName(name, Default);
        name = configDetails.ConfigName;
        var theme = Theme.Find(name).Parse(tokens);

        var current = new MagicAllSettings(name, this, theme, tokens, pageState);
        //ThemeDesigner.InitSettings(current);
        current.MagicContext = current.ThemeDesigner.BodyClasses(tokens);
        var dbg = current.DebugSources;
        dbg.Add("Name", string.Join("; ", configDetails.Source));

        _currentSettingsCache[originalNameForCache] = current;
        return current;
    }

    /// <summary>
    /// Actually internal, on the interface to avoid exposing it to the outside
    /// </summary>
    MagicSettingsCatalog IMagicSettingsService.Catalog => _catalog ??= loader.MergeCatalogs();
    private MagicSettingsCatalog? _catalog;

    private readonly NamedSettings<MagicAllSettings> _currentSettingsCache = new();

    NamedSettingsReader<MagicAnalyticsSettings> IMagicSettingsService.Analytics =>
        _analytics ??= new(this, MagicAnalyticsSettings.Defaults, cat => cat.Analytics);
    private NamedSettingsReader<MagicAnalyticsSettings>? _analytics;

    private NamedSettingsReader<MagicThemeSettings> Theme =>
        _getTheme ??= new(this, MagicThemeSettings.Defaults,
            cat => cat.Themes,
            (name) => json => json.Replace("\"=\"", $"\"{name}\"")
        );
    private NamedSettingsReader<MagicThemeSettings>? _getTheme;

    NamedSettingsReader<MagicMenuSettings> IMagicSettingsService.MenuSettings =>
        _getMenuSettings ??= new(this, MagicMenuSettings.Defaults, cat => cat.Menus);
    private NamedSettingsReader<MagicMenuSettings>? _getMenuSettings;

    NamedSettingsReader<MagicLanguagesSettings> IMagicSettingsService.Languages =>
        _languages ??= new(this, MagicLanguagesSettings.Defaults, cat => cat.Languages);
    private NamedSettingsReader<MagicLanguagesSettings>? _languages;

    internal NamedSettingsReader<MagicContainerSettings> Containers =>
        _containers ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);
    private NamedSettingsReader<MagicContainerSettings>? _containers;

    NamedSettingsReader<MagicThemeDesignSettings> IMagicSettingsService.ThemeDesign =>
        _themeDesign ??= new(this, MagicThemeDesignSettings.Defaults, cat => cat.ThemeDesigns);
    private NamedSettingsReader<MagicThemeDesignSettings>? _themeDesign;

    NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>> IMagicSettingsService.MenuDesigns =>
        _menuDesigns ??= new(this, DefaultSettings.Defaults, cat => cat.MenuDesigns);
    private NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>>? _menuDesigns;


    public (string ConfigName, List<string> Source) FindConfigName(string? configName, string inheritedName)
    {
        var debugInfo = new List<string> { $"Initial Config: '{configName}'"};
        if (configName.EqInvariant(InheritName))
        {
            configName = inheritedName;
            debugInfo.Add($"switched to inherit '{inheritedName}'");
        }
        if (configName.HasText()) return (configName, debugInfo);

        debugInfo.Add($"Config changed to '{Default}'");
        return (Default, debugInfo);
    }

    /// <summary>
    /// Exceptions - ATM just forward the loader exceptions, as none are logged here.
    /// </summary>
    public List<Exception> Exceptions => loader.Exceptions;
}