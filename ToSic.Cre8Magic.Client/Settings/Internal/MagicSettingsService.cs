using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Menus;
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

    MagicDebugSettings IMagicSettingsService.Debug => _debug ??= AllCatalogs.FirstOrDefault(c => c.Data.Debug != null)?.Data?.Debug ?? MagicDebugSettings.Defaults.Fallback;
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

    /// <summary>
    /// actually internal
    /// </summary>
    public List<DataWithJournal<MagicSettingsCatalog>> AllCatalogs => catalogsLoader.Catalogs(PackageSettings, cache: false);

    SettingsReader<MagicAnalyticsSettings> IMagicSettingsService.Analytics =>
        _analytics ??= new(
            this,
            MagicAnalyticsSettings.Defaults,
            cat => cat.Analytics
        );
    private SettingsReader<MagicAnalyticsSettings>? _analytics;

    private SettingsReader<MagicThemeSettings> ThemeSettings =>
        _getTheme ??= new(this, MagicThemeSettings.Defaults, catalog => catalog.Themes);
    private SettingsReader<MagicThemeSettings>? _getTheme;

    public MagicAnalyticsSettings AnalyticsSettings(string settingsName) => ((IMagicSettingsService)this).Analytics.FindAndNeutralize(settingsName, null, skipCache: _bypassCaches);
    
    public TDebug BypassCacheInternal<TDebug>(Func<IMagicSettingsService, TDebug> func)
    {
        this._bypassCaches = true;
        var result = func(this);
        this._bypassCaches = false;
        return result;
    }

    public SettingsReader<MagicMenuSettingsData> MenuSettings =>
        _getMenuSettings ??= new(this, MagicMenuSettingsData.Defaults, catalog => catalog.Menus);
    private SettingsReader<MagicMenuSettingsData>? _getMenuSettings;

    public SettingsReader<MagicLanguageSettingsData> Languages => _languages ??= new(this,
            MagicLanguageSettingsData.Defaults,
            catalog => catalog.Languages
        );
    private SettingsReader<MagicLanguageSettingsData>? _languages;

    public SettingsReader<MagicLanguageDesignSettings> LanguageDesigns =>
        _languageDesigns ??= new(this, MagicLanguageDesignSettings.DesignDefaults, catalog => catalog.LanguageDesigns);
    private SettingsReader<MagicLanguageDesignSettings>? _languageDesigns;

    //internal NamedSettingsReader<MagicContainerSettings> Containers =>
    //    _containers ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);
    //private NamedSettingsReader<MagicContainerSettings>? _containers;

    public SettingsReader<MagicThemeDesignSettings> ThemeDesign =>
        _themeDesign ??= new(this, MagicThemeDesignSettings.Defaults, catalog => catalog.ThemeDesigns);
    private SettingsReader<MagicThemeDesignSettings>? _themeDesign;

    public MagicThemeDesignSettings ThemeDesignSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).ThemeDesign.FindAndNeutralize(settings.Design ?? settings.Parts.GetPartSettingsNameOrFallback(nameof(settings.Design), settingsName), settingsName);

    public SettingsReader<Dictionary<string, MagicMenuDesignSettingsByName>> MenuDesigns =>
        _menuDesigns ??= new(this, DefaultSettings.Defaults, catalog => catalog.MenuDesigns);
    private SettingsReader<Dictionary<string, MagicMenuDesignSettingsByName>>? _menuDesigns;
}