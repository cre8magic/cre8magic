using System.Runtime.CompilerServices;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Analytics.Internal;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Containers.Internal;
using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Languages.Internal;
using ToSic.Cre8magic.Links;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.PageContexts.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Providers;
using ToSic.Cre8magic.UserLogins;
using ToSic.Cre8magic.UserLogins.Internal;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.Act.Internal;

internal class MagicAct(
    MagicLazy<IMagicAnalyticsService> analyticsSvc,
    MagicLazy<IMagicBreadcrumbService> breadcrumbSvc,
    IMagicSpellsService spellsSvc,
    MagicLazy<IMagicLanguageService> languageSvc,
    MagicLazy<IMagicPageContextService> pageContextSvc,
    MagicLazy<IMagicUserService> userSvc,
    MagicLazy<IUserLoginService> userKitSvc,
    MagicLazy<IMagicThemeService> themeSvc,
    MagicLazy<IMagicSettingsProvider> settingsProviderSvc,
    MagicLazy<IMagicLinkService> linkSvc,
    MagicLazy<IMagicContainerService> containerSvc,
    MagicLazy<IMagicMenuService> menuSvc) : IMagicAct
{
    #region Setup & PageState

    public IMagicAct UseThemePackage(MagicThemePackage themePackage)
    {
        spellsSvc.Setup(themePackage);
        return this;
    }

    public IMagicAct UseSpellsBook(MagicSpellsBook book)
    {
        settingsProviderSvc.Value.Provide(book);
        return this;
    }

    public IMagicAct UseSettingsProvider(Func<IMagicSettingsProvider, IMagicSettingsProvider> providerFunc)
    {
        var provider = new MagicSettingsProvider();
        var result = providerFunc(provider);
        var cat = result?.Book;
        if (cat != null)
            settingsProviderSvc.Value.Provide(cat);
        return this;
    }

    public IMagicAct UsePageState(PageState pageState)
    {
        spellsSvc.UsePageState(pageState);
        return this;
    }

    private PageState GetPageStateOrThrow(PageState? pageStateFromSettings, [CallerMemberName] string? methodName = default)
    {
        var pageState = pageStateFromSettings ?? spellsSvc.PageState;
        if (pageState == null)
            throw new ArgumentException($"PageState is required for {methodName}(...). You must either supply it in the settings, or first initialize the {nameof(MagicAct)} using {nameof(UsePageState)}(...)");
        return pageState;
    }

    #endregion

    /// <inheritdoc />
    public IMagicAnalyticsKit AnalyticsKit(MagicAnalyticsSpell? settings = null) =>
        analyticsSvc.Value.AnalyticsKit(GetPageStateOrThrow(settings?.PageState), settings);

    /// <inheritdoc />
    public IMagicBreadcrumbKit BreadcrumbKit(MagicBreadcrumbSpell? settings = null) =>
        breadcrumbSvc.Value.BreadcrumbKit(GetPageStateOrThrow(settings?.PageState), settings);

    public IMagicContainerKit ContainerKit(MagicContainerSpell? settings = default) =>
        containerSvc.Value.ContainerKit(
            GetPageStateOrThrow(settings?.PageState),
            settings?.ModuleState ?? throw new ArgumentException($"{nameof(settings.ModuleState)} is required for {nameof(ContainerKit)}(...)")
        );

    /// <inheritdoc />
    public IMagicLanguageKit LanguageKit(MagicLanguageSpell? settings = null) =>
        languageSvc.Value.LanguageKit(GetPageStateOrThrow(settings?.PageState), settings);


    /// <inheritdoc />
    public IMagicMenuKit MenuKit(MagicMenuSpell? settings = default) =>
        menuSvc.Value.MenuKit(GetPageStateOrThrow(settings?.PageState), settings);

    /// <inheritdoc />
    public IMagicPageContextKit PageContextKit(MagicPageContextSpell? settings = null) =>
        pageContextSvc.Value.PageContextKit(GetPageStateOrThrow(settings?.PageState), settings);

    /// <inheritdoc />
    public IMagicThemeKit ThemeKit(MagicThemeSpell? settings = default) =>
        themeSvc.Value.ThemeKit(GetPageStateOrThrow(settings?.PageState), settings);

    /// <inheritdoc />
    public MagicUser User(MagicUserSpell? settings = default) =>
        userSvc.Value.User(GetPageStateOrThrow(settings?.PageState));

    public IMagicUserLoginKit UserLoginKit(MagicUserLoginSpell? settings = default) =>
        userKitSvc.Value.UserLoginKit(GetPageStateOrThrow(settings?.PageState), settings);

    public string Link(MagicLinkSettings settings) =>
        linkSvc.Value.Link(GetPageStateOrThrow(settings.PageState), settings);

}