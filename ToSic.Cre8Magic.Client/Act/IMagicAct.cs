using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Links;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Spells;
using ToSic.Cre8magic.UserLogins;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.Act;

/// <summary>
/// This is the magic hat from which you can pull just about anything you can think of.
/// </summary>
public interface IMagicAct
{
    #region Setup Methods

    IMagicAct UseSpellsBook(MagicSpellsBook book);

    IMagicAct UseSettingsProvider(Func<IMagicSpellsProvider, IMagicSpellsProvider> providerFunc);
    
    IMagicAct UseThemePackage(MagicThemePackage themePackage);

    IMagicAct UsePageState(PageState pageState);

    #endregion

    /// <summary>
    /// Get the Kit to work with a Breadcrumb.
    /// It will either use the provided settings, retrieve these from the global information or use a default settings.
    /// </summary>
    IMagicBreadcrumbKit BreadcrumbKit(MagicBreadcrumbSpell? settings = default);

    /// <summary>
    /// Get the kit to work with languages. Must be async, because it might need to load async data from Oqtane.
    /// </summary>
    IMagicLanguageKit LanguageKit(MagicLanguageSpell? settings = default);


    /// <inheritdoc cref="IMagicUserService.User"/>
    MagicUser User(MagicUserSpell? settings = default);

    /// <summary>
    /// Get a kit to work with containers.
    /// </summary>
    /// <param name="settings">**Required**; must provide the ModuleState as a property and usually the PageState (if not specified in the Theme)</param>
    /// <returns></returns>
    IMagicContainerKit ContainerKit(MagicContainerSpell? settings = default);

    /// <summary>
    /// Get a kit to work with analytics.
    /// </summary>
    /// <param name="settings">Optional settings. If not provided, will try to automatically find the settings as configured in the Theme.</param>
    /// <returns></returns>
    IMagicAnalyticsKit AnalyticsKit(MagicAnalyticsSpell? settings = default);

    /// <inheritdoc cref="IMagicThemeService.ThemeKit"/>
    IMagicThemeKit ThemeKit(MagicThemeSpell? settings = default);

    IMagicUserLoginKit UserLoginKit(MagicUserLoginSpell? settings = default);


    /// <inheritdoc cref="IMagicLinkService.Link" />
    string Link(MagicLinkSettings settings);


    IMagicPageContextKit PageContextKit(MagicPageContextSpell? settings = default);

    /// <inheritdoc cref="IMagicMenuService.MenuKit" />
    IMagicMenuKit MenuKit(MagicMenuSpell? settings = default);

}