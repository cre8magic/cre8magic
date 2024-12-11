using System.Diagnostics.CodeAnalysis;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Users;
using static ToSic.Cre8magic.MagicConstants;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Service which consolidates settings made in the UI, in the JSON and falls back to coded defaults.
/// </summary>
internal class MagicSpellsService(MagicSpellsLibraryLoader libraryLoader) : IMagicSpellsService, IHasSpellsLibrary
{
    /// <inheritdoc />>
    public IMagicSpellsService Setup(MagicThemePackage themePackage)
    {
        ThemePackage = themePackage;
        ThemeTokens = null!;
        return this;
    }

    private string? Variant => ThemePackage?.Layout;

    public IMagicSpellsService UsePageState(PageState pageState)
    {
        PageState = pageState;
        return this;
    }

    /// <summary>
    /// PageState for this service, if shared/broadcast from the theme
    /// </summary>
    public PageState? PageState { get; private set; }

    public MagicDebugState DebugState(PageState pageState) =>
        ((IMagicSpellsService)this).Debug.GetState(GetThemeContext(pageState), pageState.UserIsAdmin());

    [field: AllowNull, MaybeNull]
    MagicDebugSettings IMagicSpellsService.Debug => field
        ??= Library.FirstOrDefault(c => c.Data.Debug != null)?.Data?.Debug ?? MagicDebugSettings.Defaults.Fallback;

    [field: AllowNull, MaybeNull]
    public MagicThemePackage ThemePackage { get => field ??= MagicThemePackage.Fallback; private set => field = value; }

    /// <summary>
    /// Tokens engine for this specific PageState
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    public TokenEngine PageTokenEngine(PageState pageState)
    {
        var pageFactory = new MagicPageFactory(pageState);
        var pageTokens = new PageTokens(pageFactory.Current, Variant);
        var tokens = new TokenEngine([pageTokens, ThemeTokens]);
        return tokens;
    }

    [field: AllowNull, MaybeNull]
    // ReSharper disable once RedundantAccessorBody
    private ThemeTokens ThemeTokens { get => field ??= new(ThemePackage); set => field = value; }

    #region Theme Context

    /// <inheritdoc />
    public CmThemeContext GetThemeContext(PageState? pageStateForCachingOnly)
    {
        var originalNameForCache = (Variant ?? "prevent-error") + (pageStateForCachingOnly?.Page.PageId ?? -1);
        if (pageStateForCachingOnly != null && _themeCtxCache.TryGetValue(key: originalNameForCache, value: out var cached2))
            return cached2;

        // Figure out real config-name, and get the initial layout
        var (settingsName, nameJournal) = ThemePartNameResolver.PickBestSettingsName(preferred: Variant, mainName: Default);
        var themeSettings = Themes.FindAndNeutralize([settingsName]);

        var ctx = new CmThemeContext(SettingsName: settingsName, ThemeSpell: themeSettings, Journal: nameJournal);
        if (pageStateForCachingOnly != null)
            _themeCtxCache[originalNameForCache] = ctx;
        return ctx;
    }
    private readonly Dictionary<string, CmThemeContext> _themeCtxCache = new(StringComparer.InvariantCultureIgnoreCase);

    public CmThemeContextFull GetThemeContextFull(PageState pageState)
    {
        var originalNameForCache = (Variant ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCtxFullCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        var ctxLight = GetThemeContext(pageState);

        // Tokens engine for this specific PageState
        var pageTokens = PageTokenEngine(pageState);

        var designSettings = ThemeBlueprints.FindAndNeutralize([ctxLight.ThemeSpell.Design, ctxLight.SettingsName]);
        var ctx = new CmThemeContextFull(ctxLight.SettingsName, pageState, ctxLight.ThemeSpell, designSettings, pageTokens, ctxLight.Journal);
        _themeCtxFullCache[originalNameForCache] = ctx;
        return ctx;
    }

    private readonly Dictionary<string, CmThemeContextFull> _themeCtxFullCache = new(StringComparer.InvariantCultureIgnoreCase);

    #endregion


    /// <summary>
    /// actually internal
    /// </summary>
    public List<DataWithJournal<MagicSpellsBook>> Library =>
        libraryLoader.Books(ThemePackage, cache: false);

    #region Analytics

    [field: AllowNull, MaybeNull]
    SettingsReader<MagicAnalyticsSpell> IMagicSpellsService.Analytics => field
        ??= new(this, MagicAnalyticsSpell.Defaults, cat => cat.Analytics);

    #endregion

    #region Breadcrumbs

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicBreadcrumbSpell> Breadcrumbs => field ??=
        new(this, MagicBreadcrumbSpell.Defaults, cat => cat.Breadcrumbs);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicBreadcrumbBlueprint> BreadcrumbBlueprints => field
        ??= new(this, MagicBreadcrumbBlueprint.DesignDefaults, book => book.BreadcrumbBlueprints);

    #endregion

    #region PageContexts

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicPageContextSpell> PageContexts => field
        ??= new(this, MagicPageContextSpell.Defaults, book => book.PageContexts);

    #endregion

    #region Themes

    [field: AllowNull, MaybeNull]
    private SettingsReader<MagicThemeSpell> Themes => field
        ??= new(this, MagicThemeSpell.Defaults, book => book.Themes);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicThemeBlueprint> ThemeBlueprints => field
        ??= new(this, MagicThemeBlueprint.Defaults, book => book.ThemeBlueprints);

    #endregion

    #region Languages

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicLanguageSpell> Languages => field
        ??= new(this, MagicLanguageSpell.Defaults, book => book.Languages);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicLanguageBlueprint> LanguageBlueprints => field
        ??= new(this, MagicLanguageBlueprint.DesignDefaults, book => book.LanguageBlueprints);

    #endregion

    #region Containers

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicContainerSpell> Containers => field
        ??= new(this, MagicContainerSpell.Defaults, cat => cat.Containers);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicContainerBlueprint> ContainerBlueprints => field
        ??= new(this, MagicContainerBlueprint.Defaults, cat => cat.ContainerBlueprints);

    #endregion

    #region Menus

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicMenuSpell> Menus => field
        ??= new(this, MagicMenuSpell.Defaults, book => book.Menus);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicMenuBlueprint> MenuBlueprints => field
        ??= new(this, MagicMenuBlueprint.Defaults, book => book.MenuBlueprints);

    #endregion

}