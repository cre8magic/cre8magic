using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.MagicConstants;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Service which consolidates settings made in the UI, in the JSON and falls back to coded defaults.
/// </summary>
internal class MagicSettingsService(MagicSettingsCatalogsLoader catalogsLoader) : IMagicSettingsService, IHasCatalogs
{
    /// <inheritdoc />>
    public IMagicSettingsService Setup(MagicThemePackage themePackage, string? layoutName)
    {
        _packageSettings = themePackage;
        _themeTokens = null;
        _layoutName = layoutName;
        return this;
    }

    private string? _layoutName;

    private bool _bypassCaches;

    public MagicDebugState DebugState(PageState pageState) => ((IMagicSettingsService)this).Debug.GetState(GetThemeContext(pageState), pageState.UserIsAdmin());

    MagicDebugSettings IMagicSettingsService.Debug => _debug ??= Catalogs.FirstOrDefault(c => c.Data.Debug != null)?.Data?.Debug ?? MagicDebugSettings.Defaults.Fallback;
    private MagicDebugSettings? _debug;

    private MagicThemePackage ThemePackage => _packageSettings ?? MagicThemePackage.Fallback;
    private MagicThemePackage? _packageSettings;

    /// <summary>
    /// Tokens engine for this specific PageState
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    public TokenEngine PageTokenEngine(PageState pageState)
    {
        var pageFactory = new MagicPageFactory(pageState);
        var pageTokens = new PageTokens(pageFactory.Current, _layoutName);
        var themeTokens = _themeTokens ??= new(ThemePackage);
        var tokens = new TokenEngine([pageTokens, themeTokens]);
        return tokens;
    }
    private ThemeTokens? _themeTokens;

    /// <inheritdoc />
    public CmThemeContext GetThemeContext(PageState pageState)
    {
        var originalNameForCache = (_layoutName ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCtxCache.TryGetValue(key: originalNameForCache, value: out var cached2))
            return cached2;

        // Figure out real config-name, and get the initial layout
        var (settingsName, nameJournal) = ThemePartNameResolver.PickBestSettingsName(preferred: _layoutName, mainName: Default);
        var themeSettings = Themes.FindAndNeutralize([settingsName]);

        var ctx = new CmThemeContext(SettingsName: settingsName, ThemeSettings: themeSettings, Journal: nameJournal);
        _themeCtxCache[key: originalNameForCache] = ctx;
        return ctx;
    }
    private readonly Dictionary<string, CmThemeContext> _themeCtxCache = new(StringComparer.InvariantCultureIgnoreCase);

    public CmThemeContextFull GetThemeContextFull(PageState pageState)
    {
        var originalNameForCache = (_layoutName ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCtxFullCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        var ctxLight = GetThemeContext(pageState);

        // Tokens engine for this specific PageState
        var pageTokens = PageTokenEngine(pageState);

        var designSettings = ThemeDesignSettings(ctxLight.ThemeSettings, ctxLight.SettingsName);
        var ctx = new CmThemeContextFull(ctxLight.SettingsName, pageState, ctxLight.ThemeSettings, designSettings, pageTokens, ctxLight.Journal);
        _themeCtxFullCache[originalNameForCache] = ctx;
        return ctx;
    }

    private readonly Dictionary<string, CmThemeContextFull> _themeCtxFullCache = new(StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// actually internal
    /// </summary>
    public List<DataWithJournal<MagicSettingsCatalog>> Catalogs =>
        catalogsLoader.Catalogs(ThemePackage, cache: false);

    #region Analytics - TODO: not quite done, still has a custom accessor

    SettingsReader<MagicAnalyticsSettingsData> IMagicSettingsService.Analytics =>
        _analytics ??= new(
            this,
            MagicAnalyticsSettingsData.Defaults,
            cat => cat.Analytics
        );
    private SettingsReader<MagicAnalyticsSettingsData>? _analytics;


    public MagicAnalyticsSettingsData AnalyticsSettings(string settingsName) =>
        ((IMagicSettingsService)this).Analytics.FindAndNeutralize([settingsName], skipCache: _bypassCaches);

    #endregion

    #region Breadcrumbs

    public SettingsReader<MagicBreadcrumbSettingsData> Breadcrumbs =>
        _breadcrumbs ??= new(
            this,
            MagicBreadcrumbSettingsData.Defaults,
            cat => cat.Breadcrumbs
        );
    private SettingsReader<MagicBreadcrumbSettingsData>? _breadcrumbs;

    public SettingsReader<MagicBreadcrumbDesignSettings> BreadcrumbDesigns =>
        _breadcrumbsDesigns ??= new(this, MagicBreadcrumbDesignSettings.DesignDefaults, catalog => catalog.BreadcrumbDesigns);
    private SettingsReader<MagicBreadcrumbDesignSettings>? _breadcrumbsDesigns;

    #endregion

    #region PageContexts

    public SettingsReader<MagicPageContextSettingsData> PageContexts =>
        _pageContexts ??= new(this, MagicPageContextSettingsData.Defaults, catalog => catalog.PageContexts);
    private SettingsReader<MagicPageContextSettingsData>? _pageContexts;
    

    #endregion

    private SettingsReader<MagicThemeSettings> Themes =>
        _getTheme ??= new(this, MagicThemeSettings.Defaults, catalog => catalog.Themes);
    private SettingsReader<MagicThemeSettings>? _getTheme;

    public TDebug BypassCacheInternal<TDebug>(Func<IMagicSettingsService, TDebug> func)
    {
        this._bypassCaches = true;
        var result = func(this);
        this._bypassCaches = false;
        return result;
    }

    #region Languages

    public SettingsReader<MagicLanguageSettingsData> Languages => _languages ??= new(this,
            MagicLanguageSettingsData.Defaults,
            catalog => catalog.Languages
        );
    private SettingsReader<MagicLanguageSettingsData>? _languages;

    public SettingsReader<MagicLanguageDesignSettings> LanguageDesigns =>
        _languageDesigns ??= new(this, MagicLanguageDesignSettings.DesignDefaults, catalog => catalog.LanguageDesigns);
    private SettingsReader<MagicLanguageDesignSettings>? _languageDesigns;

    #endregion

    //internal NamedSettingsReader<MagicContainerSettings> Containers =>
    //    _containers ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);
    //private NamedSettingsReader<MagicContainerSettings>? _containers;

    public SettingsReader<MagicThemeDesignSettings> ThemeDesign =>
        _themeDesign ??= new(this, MagicThemeDesignSettings.Defaults, catalog => catalog.ThemeDesigns);
    private SettingsReader<MagicThemeDesignSettings>? _themeDesign;

    public MagicThemeDesignSettings ThemeDesignSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).ThemeDesign.FindAndNeutralize(
            [
                settings.Design,
                settings.Parts.GetPartSettingsNameOrFallback(nameof(settings.Design), settingsName),
                settingsName
            ]
        );

    #region Menus

    public SettingsReader<MagicMenuSettingsData> Menus =>
        _getMenuSettings ??= new(this, MagicMenuSettingsData.Defaults, catalog => catalog.Menus);
    private SettingsReader<MagicMenuSettingsData>? _getMenuSettings;

    public SettingsReader<MagicMenuDesignSettings> MenuDesigns =>
        _menuDesigns ??= new(this, DefaultSettings.Defaults, catalog => catalog.MenuDesigns);
    private SettingsReader<MagicMenuDesignSettings>? _menuDesigns;

    #endregion

}