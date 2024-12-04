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

internal class MagicHat(
    MagicLazy<IMagicAnalyticsService> analyticsSvc,
    MagicLazy<IMagicBreadcrumbService> breadcrumbSvc,
    IMagicSettingsService settingsSvc,
    MagicLazy<IMagicLanguageService> languageSvc,
    MagicLazy<IMagicPageContextService> pageContextSvc,
    MagicLazy<IMagicUserService> userSvc,
    MagicLazy<IUserLoginService> userKitSvc,
    MagicLazy<IMagicThemeService> themeSvc,
    MagicLazy<IMagicSettingsProvider> settingsProviderSvc,
    MagicLazy<IMagicLinkService> linkSvc,
    MagicLazy<IMagicContainerService> containerSvc,
    MagicLazy<IMagicMenuService> menuSvc) : IMagicHat
{
    #region Setup & PageState

    public IMagicHat UseSettingsPackage(MagicThemePackage themePackage)
    {
        settingsSvc.Setup(themePackage);
        return this;
    }

    public IMagicHat UseSettingsCatalog(MagicSettingsCatalog catalog)
    {
        settingsProviderSvc.Value.Provide(catalog);
        return this;
    }

    public IMagicHat UseSettingsProvider(Func<IMagicSettingsProvider, IMagicSettingsProvider> providerFunc)
    {
        var provider = new MagicSettingsProvider();
        var result = providerFunc(provider);
        var cat = result?.Catalog;
        if (cat != null)
            settingsProviderSvc.Value.Provide(cat);
        return this;
    }

    public IMagicHat UsePageState(PageState pageState)
    {
        settingsSvc.UsePageState(pageState);
        return this;
    }

    private PageState GetPageStateOrThrow(PageState? pageStateFromSettings, [CallerMemberName] string? methodName = default)
    {
        var pageState = pageStateFromSettings ?? settingsSvc.PageState;
        if (pageState == null)
            throw new ArgumentException($"PageState is required for {methodName}(...). You must either supply it in the settings, or first initialize the MagicHat using {nameof(UsePageState)}(...)");
        return pageState;
    }

    #endregion

    /// <inheritdoc />
    public IMagicAnalyticsKit AnalyticsKit(MagicAnalyticsSettings? settings = null) =>
        analyticsSvc.Value.AnalyticsKit(GetPageStateOrThrow(settings?.PageState), settings);

    /// <inheritdoc />
    public IMagicBreadcrumbKit BreadcrumbKit(MagicBreadcrumbSettings? settings = null) =>
        breadcrumbSvc.Value.BreadcrumbKit(GetPageStateOrThrow(settings?.PageState), settings);

    /// <inheritdoc />
    public IMagicLanguageKit LanguageKit(MagicLanguageSettings? settings = null) =>
        languageSvc.Value.LanguageKit(GetPageStateOrThrow(settings?.PageState), settings);


    /// <inheritdoc />
    public IMagicPageContextKit PageContextKit(MagicPageContextSettings? settings = null) =>
        pageContextSvc.Value.PageContextKit(GetPageStateOrThrow(settings?.PageState), settings);

    /// <inheritdoc />
    public MagicUser User(PageState pageState) =>
        userSvc.Value.User(pageState);

    /// <inheritdoc />
    public IMagicMenuKit MenuKit(MagicMenuSettings? settings = default) =>
        menuSvc.Value.MenuKit(GetPageStateOrThrow(settings?.PageState), settings);
    

    public IMagicUserLoginKit UserLoginKit(MagicUserLoginSettings? settings = default) =>
        userKitSvc.Value.UserLoginKit(GetPageStateOrThrow(settings?.PageState), settings);

    public IMagicContainerKit ContainerKit(MagicContainerSettings settings) =>
        containerSvc.Value.ContainerKit(
            GetPageStateOrThrow(settings?.PageState),
            settings?.ModuleState ?? throw new ArgumentException($"{nameof(settings.ModuleState)} is required for {nameof(ContainerKit)}(...)")
        );

    public string Link(PageState pageState, MagicLinkSettings settings) =>
        linkSvc.Value.Link(pageState, settings);

    /// <inheritdoc />
    public IMagicThemeKit ThemeKit(MagicThemeSettings? settings = default) =>
        themeSvc.Value.ThemeKit(GetPageStateOrThrow(settings?.PageState), settings);
}