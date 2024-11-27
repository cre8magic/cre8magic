using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Languages.Internal;
using ToSic.Cre8magic.Links;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic;

/// <summary>
/// This is the magic hat from which you can pull just about anything you can think of.
/// </summary>
public interface IMagicHat
{
    #region Setup Methods

    IMagicHat UseSettingsCatalog(MagicSettingsCatalog catalog);

    IMagicHat UseSettingsProvider(Func<IMagicSettingsProvider, IMagicSettingsProvider> providerFunc);
    
    IMagicHat UseSettingsPackage(MagicThemePackage themePackage, string? layoutName = default);

    IMagicHat UsePageState(PageState pageState);

    #endregion

    /// <inheritdoc cref="IMagicBreadcrumbService.BreadcrumbKit"/>
    IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = default);

    /// <inheritdoc cref="IMagicLanguageService.LanguageKitAsync"/>
    Task<IMagicLanguageKit> LanguageKitAsync(PageState pageState, MagicLanguageSettings? settings = default);


    /// <inheritdoc cref="IMagicUserService.User"/>
    MagicUser User(PageState pageState);

    /// <inheritdoc cref="IMagicContainerService.ContainerKit"/>
    IMagicContainerKit ContainerKit(PageState pageState, Module module, MagicContainerSettings? settings = default);

    /// <inheritdoc cref="IMagicAnalyticsService.AnalyticsKit"/>
    IMagicAnalyticsKit AnalyticsKit(MagicAnalyticsSettings? settings = default);

    /// <inheritdoc cref="IMagicThemeService.ThemeKit"/>
    IMagicThemeKit ThemeKit(PageState pageState);

    IMagicUserLoginKit UserLoginKit(PageState pageState);


    string Link(PageState pageState, MagicLinkSpecs linkSpecs);
    IMagicPageContextKit PageContextKit(PageState pageState, MagicPageContextSettings? settings = default);
}