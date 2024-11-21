using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Themes;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.MagicConstants;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Service which consolidates settings made in the UI, in the JSON and falls back to coded defaults.
/// </summary>
internal class MagicSettingsService(MagicSettingsCatalogsLoader catalogsLoader) : IMagicSettingsService
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

    /// <inheritdoc />
    public MagicThemeContext GetThemeContext(PageState pageState)
    {
        var originalNameForCache = (_layoutName ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCtxCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        // Figure out real config-name, and get the initial layout
        var (settingsName, nameJournal) = ThemePartNameResolver.PickBestSettingsName(_layoutName, Default);
        var themeSettings = ThemeSettings.FindAndNeutralize(settingsName);

        var ctx = new MagicThemeContext(settingsName, themeSettings, nameJournal);
        _themeCtxCache[originalNameForCache] = ctx;
        return ctx;
    }
    private readonly Dictionary<string, MagicThemeContext> _themeCtxCache = new(StringComparer.InvariantCultureIgnoreCase);

    public MagicThemeContextFull GetThemeContextFull(PageState pageState)
    {
        var originalNameForCache = (_layoutName ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCtxFullCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        var ctxLight = GetThemeContext(pageState);

        // Tokens engine for this specific PageState
        var pageTokens = PageTokenEngine(pageState);

        var designSettings = ThemeDesignSettings(ctxLight.ThemeSettings, ctxLight.SettingsName);
        var ctx = new MagicThemeContextFull(ctxLight.SettingsName, pageState, ctxLight.ThemeSettings, designSettings, pageTokens, ctxLight.Journal);
        _themeCtxFullCache[originalNameForCache] = ctx;
        return ctx;
    }

    private readonly Dictionary<string, MagicThemeContextFull> _themeCtxFullCache = new(StringComparer.InvariantCultureIgnoreCase);

    // #WipRemovingPreMergedCatalog
    ///// <summary>
    ///// Actually internal, on the interface to avoid exposing it to the outside
    ///// </summary>
    //public MagicSettingsCatalog Catalog => _catalog ??= loader.MergeCatalogs(PackageSettings);
    //private MagicSettingsCatalog? _catalog;

    /// <summary>
    /// actually internal
    /// </summary>
    public List<MagicSettingsCatalog> AllCatalogs => catalogsLoader.Catalogs(PackageSettings, cache: false);

    NamedSettingsReader<MagicAnalyticsSettings> IMagicSettingsService.Analytics =>
        _analytics ??= new(
            this,
            MagicAnalyticsSettings.Defaults,
            cat => cat.Analytics
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

    NamedSettingsReader<MagicMenuSettingsData> IMagicSettingsService.MenuSettings =>
        _getMenuSettings ??= new(this, MagicMenuSettingsData.Defaults, cat => cat.Menus);
    private NamedSettingsReader<MagicMenuSettingsData>? _getMenuSettings;

    NamedSettingsReader<MagicLanguageSettings> IMagicSettingsService.Languages =>
        _languages ??= new(this, MagicLanguageSettings.Defaults, cat => cat.Languages);
    private NamedSettingsReader<MagicLanguageSettings>? _languages;

    public MagicLanguageSettings LanguageSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).Languages.FindAndNeutralize(settings.Parts.GetPartSettingsNameOrFallback("Languages", settingsName), settingsName);

    //internal NamedSettingsReader<MagicContainerSettings> Containers =>
    //    _containers ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);
    //private NamedSettingsReader<MagicContainerSettings>? _containers;

    NamedSettingsReader<MagicThemeDesignSettings> IMagicSettingsService.ThemeDesign =>
        _themeDesign ??= new(this, MagicThemeDesignSettings.Defaults, cat => cat.ThemeDesigns);
    private NamedSettingsReader<MagicThemeDesignSettings>? _themeDesign;

    public MagicThemeDesignSettings ThemeDesignSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).ThemeDesign.FindAndNeutralize(settings.Design ?? settings.Parts.GetPartSettingsNameOrFallback(nameof(settings.Design), settingsName), settingsName);

    NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>> IMagicSettingsService.MenuDesigns =>
        _menuDesigns ??= new(this, DefaultSettings.Defaults, cat => cat.MenuDesigns);
    private NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>>? _menuDesigns;

    /// <summary>
    /// Exceptions - ATM just forward the loader exceptions, as none are logged here.
    /// </summary>
    public List<Exception> Exceptions => catalogsLoader.Exceptions;
}