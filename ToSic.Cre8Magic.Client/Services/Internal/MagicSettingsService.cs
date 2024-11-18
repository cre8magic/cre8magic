using Microsoft.Extensions.Logging;
using Oqtane.UI;
using System.Xml.Linq;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.Client.MagicConstants;

namespace ToSic.Cre8magic.Services.Internal;

/// <summary>
/// Service which consolidates settings made in the UI, in the JSON and falls back to coded defaults.
/// </summary>
internal class MagicSettingsService(ILogger<IMagicSettingsService> logger, MagicSettingsLoader loader)
    : IMagicSettingsService
{
    /// <inheritdoc />>
    public IMagicSettingsService Setup(MagicPackageSettings packageSettings, string? layoutName, string? bodyClasses)
    {
        _packageSettings = packageSettings;
        loader.Setup(packageSettings);
        _themeTokens = null;
        _currentSettingsCache.Clear();
        _layoutName = layoutName;
        _bodyClasses = bodyClasses;
        return this;
    }

    private string? _bodyClasses;
    private string? _layoutName;

    MagicDebugSettings IMagicSettingsService.Debug => _debug ??= loader.DebugSettings ?? MagicDebugSettings.Defaults.Fallback;
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


    /// <summary>
    /// Logger, provided to the <see cref="NamedSettingsReader{TPart}"/>
    /// </summary>
    ILogger<IMagicSettingsService> IMagicSettingsService.Logger { get; } = logger;

    public MagicAllSettings GetSettings(PageState pageState)
    {
        // Check if already in cache; vary by layout name and active page
        var originalNameForCache = (_layoutName ?? "prevent-error") + pageState.Page.PageId;
        if (_currentSettingsCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        var themeCtx = GetThemeContext(pageState);

        // Get the magic context (probably the classes we'll add) using the tokens
        var themeDesigner = new MagicThemeDesigner(themeCtx);

        var current = new MagicAllSettings(themeCtx.SettingsName, this, themeCtx.ThemeSettings, themeDesigner, themeCtx.PageTokens, pageState)
            {
                MagicContext = themeDesigner.BodyClasses(themeCtx.PageTokens, _bodyClasses) ?? ""
            };

        // Cache and return
        _currentSettingsCache[originalNameForCache] = current;
        return current;
    }
    private readonly NamedSettings<MagicAllSettings> _currentSettingsCache = new();

    public MagicThemeContext GetThemeContext(PageState pageState)
    {
        var originalNameForCache = (_layoutName ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        // Tokens engine for this specific PageState
        var tokens = PageTokenEngine(pageState);

        // Figure out real config-name, and get the initial layout
        var (settingsName, nameJournal) = MagicAllSettingsReader.GetBestSettingsName(_layoutName, Default);
        var theme = ThemeSettings.Find(settingsName).Parse(tokens);

        var designSettings = ThemeDesignSettings(theme, settingsName);
        var ctx = new MagicThemeContext(settingsName, pageState, theme, designSettings, tokens, nameJournal);
        _themeCache[originalNameForCache] = ctx;
        return ctx;
    }

    private readonly Dictionary<string, MagicThemeContext> _themeCache = new(StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// Actually internal, on the interface to avoid exposing it to the outside
    /// </summary>
    MagicSettingsCatalog IMagicSettingsService.Catalog => _catalog ??= loader.MergeCatalogs();
    private MagicSettingsCatalog? _catalog;


    NamedSettingsReader<MagicAnalyticsSettings> IMagicSettingsService.Analytics =>
        _analytics ??= new(this, MagicAnalyticsSettings.Defaults, cat => cat.Analytics);
    private NamedSettingsReader<MagicAnalyticsSettings>? _analytics;

    private NamedSettingsReader<MagicThemeSettings> ThemeSettings =>
        _getTheme ??= new(this, MagicThemeSettings.Defaults,
            cat => cat.Themes,

            // Special processing, to swap default design term "=" which means it's the same as the name
            modify: (name, settings) =>
            {
                // If settings has a design which should match the name, insert it now
                settings = settings.Design == InheritName ? settings with { Design = name } : settings;
                if (settings.Parts.Count == 0)
                    return settings;

                var modParts = settings.Parts.ToDictionary(
                    p => p.Key,
                    p => p.Value.Design == InheritName ? p.Value with { Design = name } : p.Value
                );

                settings = settings with { Parts = new(modParts) };

                return settings;
            });
    private NamedSettingsReader<MagicThemeSettings>? _getTheme;

    NamedSettingsReader<MagicMenuSettings> IMagicSettingsService.MenuSettings =>
        _getMenuSettings ??= new(this, MagicMenuSettings.Defaults, cat => cat.Menus);
    private NamedSettingsReader<MagicMenuSettings>? _getMenuSettings;

    NamedSettingsReader<MagicLanguageSettings> IMagicSettingsService.Languages =>
        _languages ??= new(this, MagicLanguageSettings.Defaults, cat => cat.Languages);
    private NamedSettingsReader<MagicLanguageSettings>? _languages;

    public MagicLanguageSettings LanguageSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).Languages.Find(settings.Parts.GetPartRenameOrFallback("Languages", settingsName), settingsName);

    internal NamedSettingsReader<MagicContainerSettings> Containers =>
        _containers ??= new(this, MagicContainerSettings.Defaults, cat => cat.Containers);
    private NamedSettingsReader<MagicContainerSettings>? _containers;

    NamedSettingsReader<MagicThemeDesignSettings> IMagicSettingsService.ThemeDesign =>
        _themeDesign ??= new(this, MagicThemeDesignSettings.Defaults, cat => cat.ThemeDesigns);
    private NamedSettingsReader<MagicThemeDesignSettings>? _themeDesign;

    public MagicThemeDesignSettings ThemeDesignSettings(MagicThemeSettings settings, string settingsName) =>
        ((IMagicSettingsService)this).ThemeDesign.Find(settings.Design ?? settings.Parts.GetPartRenameOrFallback(nameof(settings.Design), settingsName), settingsName);

    NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>> IMagicSettingsService.MenuDesigns =>
        _menuDesigns ??= new(this, DefaultSettings.Defaults, cat => cat.MenuDesigns);
    private NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>>? _menuDesigns;

    /// <summary>
    /// Exceptions - ATM just forward the loader exceptions, as none are logged here.
    /// </summary>
    public List<Exception> Exceptions => loader.Exceptions;
}