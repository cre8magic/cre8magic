using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
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

    public IMagicSettingsService UsePageState(PageState pageState)
    {
        PageState = pageState;
        return this;
    }

    /// <summary>
    /// WIP: PageState for this service
    /// </summary>
    public PageState? PageState { get; private set; }

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

    #region Theme Context

    /// <inheritdoc />
    public CmThemeContext GetThemeContext(PageState pageStateForCachingOnly)
    {
        var originalNameForCache = (_layoutName ?? "prevent-error") + (pageStateForCachingOnly?.Page.PageId ?? -1);
        if (pageStateForCachingOnly != null && _themeCtxCache.TryGetValue(key: originalNameForCache, value: out var cached2))
            return cached2;

        // Figure out real config-name, and get the initial layout
        var (settingsName, nameJournal) = ThemePartNameResolver.PickBestSettingsName(preferred: _layoutName, mainName: Default);
        var themeSettings = Themes.FindAndNeutralize([settingsName]);

        var ctx = new CmThemeContext(SettingsName: settingsName, ThemeSettings: themeSettings, Journal: nameJournal);
        if (pageStateForCachingOnly != null)
            _themeCtxCache[originalNameForCache] = ctx;
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

        var designSettings = ThemeDesigns.FindAndNeutralize([ctxLight.ThemeSettings.Design, ctxLight.SettingsName]);
        var ctx = new CmThemeContextFull(ctxLight.SettingsName, pageState, ctxLight.ThemeSettings, designSettings, pageTokens, ctxLight.Journal);
        _themeCtxFullCache[originalNameForCache] = ctx;
        return ctx;
    }

    private readonly Dictionary<string, CmThemeContextFull> _themeCtxFullCache = new(StringComparer.InvariantCultureIgnoreCase);

    #endregion


    /// <summary>
    /// actually internal
    /// </summary>
    public List<DataWithJournal<MagicSettingsCatalog>> Catalogs =>
        catalogsLoader.Catalogs(ThemePackage, cache: false);

    #region Analytics

    SettingsReader<MagicAnalyticsSettings> IMagicSettingsService.Analytics =>
        _analytics ??= new(
            this,
            MagicAnalyticsSettings.Defaults,
            cat => cat.Analytics
        );
    private SettingsReader<MagicAnalyticsSettings>? _analytics;
    
    #endregion

    #region Breadcrumbs

    public SettingsReader<MagicBreadcrumbSettings> Breadcrumbs =>
        _breadcrumbs ??= new(
            this,
            MagicBreadcrumbSettings.Defaults,
            cat => cat.Breadcrumbs
        );
    private SettingsReader<MagicBreadcrumbSettings>? _breadcrumbs;

    public SettingsReader<MagicBreadcrumbDesignSettings> BreadcrumbDesigns =>
        _breadcrumbsDesigns ??= new(this, MagicBreadcrumbDesignSettings.DesignDefaults, catalog => catalog.BreadcrumbDesigns);
    private SettingsReader<MagicBreadcrumbDesignSettings>? _breadcrumbsDesigns;

    #endregion

    #region PageContexts

    public SettingsReader<MagicPageContextSettings> PageContexts =>
        _pageContexts ??= new(this, MagicPageContextSettings.Defaults, catalog => catalog.PageContexts);
    private SettingsReader<MagicPageContextSettings>? _pageContexts;


    #endregion

    #region Themes

    private SettingsReader<MagicThemeSettings> Themes =>
        _getTheme ??= new(this, MagicThemeSettings.Defaults, catalog => catalog.Themes);
    private SettingsReader<MagicThemeSettings>? _getTheme;

    public SettingsReader<MagicThemeDesignSettings> ThemeDesigns =>
        _themeDesigns ??= new(this, MagicThemeDesignSettings.Defaults, catalog => catalog.ThemeDesigns);
    private SettingsReader<MagicThemeDesignSettings>? _themeDesigns;

    #endregion

    #region Languages

    public SettingsReader<MagicLanguageSettings> Languages => _languages ??= new(this,
            MagicLanguageSettings.Defaults,
            catalog => catalog.Languages
        );
    private SettingsReader<MagicLanguageSettings>? _languages;

    public SettingsReader<MagicLanguageDesignSettings> LanguageDesigns =>
        _languageDesigns ??= new(this, MagicLanguageDesignSettings.DesignDefaults, catalog => catalog.LanguageDesigns);
    private SettingsReader<MagicLanguageDesignSettings>? _languageDesigns;

    #endregion

    #region Containers

    public SettingsReader<MagicContainerSettings> Containers =>
        _containers ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);
    private SettingsReader<MagicContainerSettings>? _containers;

    public SettingsReader<MagicContainerDesignSettings> ContainerDesigns =>
        _containerDesigns ??= new(this, MagicContainerDesignSettings.Defaults, cat => cat.ContainerDesigns);
    private SettingsReader<MagicContainerDesignSettings>? _containerDesigns;


    #endregion

    #region Menus

    public SettingsReader<MagicMenuSettings> Menus =>
        _getMenuSettings ??= new(this, MagicMenuSettings.Defaults, catalog => catalog.Menus);
    private SettingsReader<MagicMenuSettings>? _getMenuSettings;

    public SettingsReader<MagicMenuDesignSettings> MenuDesigns =>
        _menuDesigns ??= new(this, DefaultSettings.Defaults, catalog => catalog.MenuDesigns);
    private SettingsReader<MagicMenuDesignSettings>? _menuDesigns;

    #endregion

}