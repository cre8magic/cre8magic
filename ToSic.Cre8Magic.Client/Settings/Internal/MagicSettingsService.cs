using System.Diagnostics.CodeAnalysis;
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
        ThemeTokens = null!;
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
    /// PageState for this service, if shared/broadcast from the theme
    /// </summary>
    public PageState? PageState { get; private set; }

    public MagicDebugState DebugState(PageState pageState) =>
        ((IMagicSettingsService)this).Debug.GetState(GetThemeContext(pageState), pageState.UserIsAdmin());

    [field: AllowNull, MaybeNull]
    MagicDebugSettings IMagicSettingsService.Debug => field
        ??= Catalogs.FirstOrDefault(c => c.Data.Debug != null)?.Data?.Debug ?? MagicDebugSettings.Defaults.Fallback;

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
        var tokens = new TokenEngine([pageTokens, ThemeTokens]);
        return tokens;
    }

    [field: AllowNull, MaybeNull]
    private ThemeTokens ThemeTokens { get => field ??= new(ThemePackage); set; }

    #region Theme Context

    /// <inheritdoc />
    public CmThemeContext GetThemeContext(PageState? pageStateForCachingOnly)
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

    [field: AllowNull, MaybeNull]
    SettingsReader<MagicAnalyticsSettings> IMagicSettingsService.Analytics => field
        ??= new(this, MagicAnalyticsSettings.Defaults, cat => cat.Analytics);

    #endregion

    #region Breadcrumbs

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicBreadcrumbSettings> Breadcrumbs => field ??=
        new(this, MagicBreadcrumbSettings.Defaults, cat => cat.Breadcrumbs);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicBreadcrumbDesignSettings> BreadcrumbDesigns => field
        ??= new(this, MagicBreadcrumbDesignSettings.DesignDefaults, catalog => catalog.BreadcrumbDesigns);

    #endregion

    #region PageContexts

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicPageContextSettings> PageContexts => field
        ??= new(this, MagicPageContextSettings.Defaults, catalog => catalog.PageContexts);

    #endregion

    #region Themes

    [field: AllowNull, MaybeNull]
    private SettingsReader<MagicThemeSettings> Themes => field
        ??= new(this, MagicThemeSettings.Defaults, catalog => catalog.Themes);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicThemeDesignSettings> ThemeDesigns => field
        ??= new(this, MagicThemeDesignSettings.Defaults, catalog => catalog.ThemeDesigns);

    #endregion

    #region Languages

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicLanguageSettings> Languages => field
        ??= new(this, MagicLanguageSettings.Defaults, catalog => catalog.Languages);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicLanguageDesignSettings> LanguageDesigns => field
        ??= new(this, MagicLanguageDesignSettings.DesignDefaults, catalog => catalog.LanguageDesigns);

    #endregion

    #region Containers

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicContainerSettings> Containers => field
        ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicContainerDesignSettings> ContainerDesigns => field
        ??= new(this, MagicContainerDesignSettings.Defaults, cat => cat.ContainerDesigns);

    #endregion

    #region Menus

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicMenuSettings> Menus => field
        ??= new(this, MagicMenuSettings.Defaults, catalog => catalog.Menus);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicMenuDesignSettings> MenuDesigns => field
        ??= new(this, DefaultSettings.Defaults, catalog => catalog.MenuDesigns);

    #endregion

}