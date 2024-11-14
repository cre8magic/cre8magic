using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumb.Settings;

namespace ToSic.Cre8magic.Pages;

public interface IMagicPageService
{
    IEnumerable<IMagicPage> GetAll(PageState pageState, bool ignorePermissions = default);

    IMagicPage GetHome(PageState pageState);

    IMagicPage GetCurrent(PageState pageState);

    IMagicPage? GetPage(PageState pageState, int pageId);

    IMagicPage? GetPage(PageState pageState, Page? page);

    IEnumerable<IMagicPage> GetPages(PageState pageState, IEnumerable<int> pageIds);

    IMagicPageList GetBreadcrumb(PageState pageState, MagicBreadcrumbSettings? specs = default);

}