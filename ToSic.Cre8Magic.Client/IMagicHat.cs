using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Languages.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic;

/// <summary>
/// This is the magic hat from which you can pull just about anything you can think of.
/// </summary>
public interface IMagicHat
{
    /// <inheritdoc cref="IMagicBreadcrumbService.BreadcrumbKit"/>
    IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = default);

    /// <inheritdoc cref="IMagicLanguageService.LanguageKitAsync"/>
    Task<IMagicLanguageKit> LanguageKitAsync(PageState pageState, MagicLanguageSettings? settings = default);


    #region OLD - to be replaced!



    public MagicContainerDesigner ContainerDesigner(PageState pageState, Module module);
    public MagicThemeDesigner ThemeDesigner(PageState pageState);

    //internal MagicLanguageDesigner LanguageDesigner(PageState pageState);

    #endregion

    MagicUser User(PageState pageState);
    IMagicContainerKit ContainerKit(PageState pageState, Module module);
}