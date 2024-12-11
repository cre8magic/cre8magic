using Oqtane.UI;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

public interface IMagicBreadcrumbService
{
    /// <summary>
    /// Get the Kit to work with a Breadcrumb.
    /// It will either use the provided settings, retrieve these from the global information or use a default settings.
    /// </summary>
    /// <param name="pageState"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSpell? settings = default);
}