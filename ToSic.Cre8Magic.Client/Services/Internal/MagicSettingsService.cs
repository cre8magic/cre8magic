using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.Client.MagicConstants;

namespace ToSic.Cre8magic.Services.Internal;

/// <summary>
/// Service which consolidates settings made in the UI, in the JSON and falls back to coded defaults.
/// </summary>
internal class MagicSettingsService(MagicSettingsLoader loader) : IMagicSettingsService
{
    /// <inheritdoc />>
    public IMagicSettingsService Setup(MagicPackageSettings packageSettings, string? layoutName)
    {
        _packageSettings = packageSettings;
        _themeTokens = null;
        _layoutName = layoutName;
        return this;
    }

    private string? _layoutName;

    private bool _bypassCaches;

    public MagicDebugState DebugState(PageState pageState) => ((IMagicSettingsService)this).Debug.GetState(GetThemeContext(pageState), pageState.UserIsAdmin());

    MagicDebugSettings IMagicSettingsService.Debug => _debug ??= AllCatalogs.FirstOrDefault(c => c.Debug != null)?.Debug ?? MagicDebugSettings.Defaults.Fallback;
    private MagicDebugSettings? _debug;

    private MagicPackageSettings PackageSettings => _packageSettings ?? MagicPackageSettings.Fallback;
    private MagicPackageSettings? _packageSettings;

    /// <summary>
    /// Tokens engine for this specific PageState
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    public TokenEngine PageTokenEngine(PageState pageState)
    {
        var pageFactory = new MagicPageFactory(pageState);
        var pageTokens = new PageTokens(pageFactory.Current, _layoutName);
        var themeTokens = _themeTokens ??= new(PackageSettings);
        var tokens = new TokenEngine([pageTokens, themeTokens]);
        return tokens;
    }
    private ThemeTokens? _themeTokens;


    public MagicThemeContext GetThemeContext(PageState pageState)
    {
        var originalNameForCache = (_layoutName ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        // Tokens engine for this specific PageState
        var tokens = PageTokenEngine(pageState);

        // Figure out real config-name, and get the initial layout
        var (settingsName, nameJournal) = ThemePartNameResolver.GetBestSettingsName(_layoutName, Default);
        var themeSettings = ThemeSettings.FindAndNeutralize(settingsName);
        var theme = themeSettings with
        {
            Logo = tokens.Parse(themeSettings.Logo)
        };

        var designSettings = ThemeDesignSettings(theme, settingsName);
        var ctx = new MagicThemeContext(settingsName, pageState, theme, designSettings, tokens, nameJournal);
        _themeCache[originalNameForCache] = ctx;
        return ctx;
    }

    private readonly Dictionary<string, MagicThemeContext> _themeCache = new(StringComparer.InvariantCultureIgnoreCase);

    // #WipRemovingPreMergedCatalog
    ///// <summary>
    ///// Actually internal, on the interface to avoid exposing it to the outside
    ///// </summary>
    //public MagicSettingsCatalog Catalog => _catalog ??= loader.MergeCatalogs(PackageSettings);
    //private MagicSettingsCatalog? _catalog;

    /// <summary>
    /// actually internal
    /// </summary>
    public List<MagicSettingsCatalog> AllCatalogs => loader.Catalogs(PackageSettings, cache: false);

    NamedSettingsReader<MagicAnalyticsSettings> IMagicSettingsService.Analytics =>
        _analytics ??= new(
            this,
            MagicAnalyticsSettings.Defaults,
            cat => cat.Analytics,
            useAllSources: true
        );
    private NamedSettingsReader<MagicAnalyticsSettings>? _analytics;

    private NamedSettingsReader<MagicThemeSettings> ThemeSettings =>
        _getTheme ??= new(this, MagicThemeSettings.Defaults, cat => cat.Themes);
    private NamedSettingsReader<MagicThemeSettings>? _getTheme;

    public MagicAnalyticsSettings AnalyticsSettings(string settingsName) => ((IMagicSettingsService)this).Analytics.FindAndNeutralize(settingsName, null, skipCache: _bypassCaches);
    
    public TDebug BypassCacheInternal<TDebug>(Func<IMagicSettingsService, TDebug> func)
    {
        this._bypassCaches = true;
        var result = func(this);
        this._bypassCaches = false;
        return result;
    }

    NamedSettingsReader<MagicMenuSettings> IMagicSettingsService.MenuSettings =>
        _getMenuSettings ??= new(this, MagicMenuSettings.Defaults, cat => cat.Menus);
    private NamedSettingsReader<MagicMenuSettings>? _getMenuSettings;

    NamedSettingsReader<MagicLanguageSettings> IMagicSettingsService.Languages =>
        _languages ??= new(this, MagicLanguageSettings.Defaults, cat => cat.Languages);
    private NamedSettingsReader<MagicLanguageSettings>? _languages;

    public MagicLanguageSettings LanguageSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).Languages.FindAndNeutralize(settings.Parts.GetPartRenameOrFallback("Languages", settingsName), settingsName);

    //internal NamedSettingsReader<MagicContainerSettings> Containers =>
    //    _containers ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);
    //private NamedSettingsReader<MagicContainerSettings>? _containers;

    NamedSettingsReader<MagicThemeDesignSettings> IMagicSettingsService.ThemeDesign =>
        _themeDesign ??= new(this, MagicThemeDesignSettings.Defaults, cat => cat.ThemeDesigns);
    private NamedSettingsReader<MagicThemeDesignSettings>? _themeDesign;

    public MagicThemeDesignSettings ThemeDesignSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).ThemeDesign.FindAndNeutralize(settings.Design ?? settings.Parts.GetPartRenameOrFallback(nameof(settings.Design), settingsName), settingsName);

    NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>> IMagicSettingsService.MenuDesigns =>
        _menuDesigns ??= new(this, DefaultSettings.Defaults, cat => cat.MenuDesigns);
    private NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>>? _menuDesigns;

    /// <summary>
    /// Exceptions - ATM just forward the loader exceptions, as none are logged here.
    /// </summary>
    public List<Exception> Exceptions => loader.Exceptions;
}