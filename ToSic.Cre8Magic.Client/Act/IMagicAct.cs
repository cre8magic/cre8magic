using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Links;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Providers;
using ToSic.Cre8magic.UserLogins;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.Act;

/// <summary>
/// This is the magic hat from which you can pull just about anything you can think of.
/// </summary>
public interface IMagicAct
{
    #region Setup Methods

    /// <summary>
    /// From now on, use the specified book for looking up settings.
    /// </summary>
    /// <remarks>
    /// This is Blazor-Scoped.
    /// It will affect everything that happens afterward until the page is completely reloaded.
    /// You should only call this from the Theme and never from another component, as it will affect everything (unexpected).
    /// </remarks>
    /// <param name="book"></param>
    /// <returns></returns>
    IMagicAct UseBook(MagicBook book);
    
    IMagicAct UseThemePackage(MagicThemePackage themePackage);

    /// <summary>
    /// Give the MagicAct the current PageState to work with.
    /// </summary>
    /// <remarks>
    /// This is Blazor-Scoped. It will affect everything that happens afterward until the page is completely reloaded.
    /// You should only call this from the Theme and never from another component, as it will affect everything (unexpected).
    /// </remarks>
    /// <param name="pageState"></param>
    /// <returns></returns>
    IMagicAct UsePageState(PageState pageState);

    #endregion

    /// <summary>
    /// Get the Kit to work with a Breadcrumb.
    /// It will either use the provided settings, retrieve these from the global information or use a default settings.
    /// </summary>
    IMagicBreadcrumbKit BreadcrumbKit(MagicBreadcrumbSettings? settings = default);

    /// <summary>
    /// Get the kit to work with languages. Must be async, because it might need to load async data from Oqtane.
    /// </summary>
    IMagicLanguageKit LanguageKit(MagicLanguageSettings? settings = default);


    /// <inheritdoc cref="IMagicUserService.User"/>
    MagicUser User(MagicUserSettings? settings = default);

    /// <summary>
    /// Get a kit to work with containers.
    /// </summary>
    /// <param name="settings">**Required**; must provide the ModuleState as a property and usually the PageState (if not specified in the Theme)</param>
    /// <returns></returns>
    IMagicContainerKit ContainerKit(MagicContainerSettings? settings = default);

    /// <summary>
    /// Get a kit to work with analytics.
    /// </summary>
    /// <param name="settings">Optional settings. If not provided, will try to automatically find the settings as configured in the Theme.</param>
    /// <returns></returns>
    IMagicAnalyticsKit AnalyticsKit(MagicAnalyticsSettings? settings = default);

    /// <inheritdoc cref="IMagicThemeService.ThemeKit"/>
    IMagicThemeKit ThemeKit(MagicThemeSettings? settings = default);

    IMagicUserLoginKit UserLoginKit(MagicUserLoginSettings? settings = default);


    /// <inheritdoc cref="IMagicLinkService.Link" />
    string Link(MagicLinkSettings settings);


    IMagicPageContextKit PageContextKit(MagicPageContextSettings? settings = default);

    /// <inheritdoc cref="IMagicMenuService.MenuKit" />
    IMagicMenuKit MenuKit(MagicMenuSettings? settings = default);

}