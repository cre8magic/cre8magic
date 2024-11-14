using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumb.Settings;

namespace ToSic.Cre8magic.Pages;

public interface IMagicPageService
{
    /// <summary>
    /// The page state - must be initialized before using the service.
    /// </summary>
    /// <remarks>
    /// Will throw an error if accessed before initializing.
    /// </remarks>
    //internal PageState PageState { get; }

    //IMagicPageService Setup(PageState pageState);

    IEnumerable<IMagicPage> GetAll(PageState pageState, bool ignorePermissions = default);

    IMagicPage GetHome(PageState pageState);

    IMagicPage GetCurrent(PageState pageState);

    IMagicPage? GetPage(PageState pageState, int pageId);

    IMagicPage? GetPage(PageState pageState, Page? page);

    IEnumerable<IMagicPage> GetPages(PageState pageState, IEnumerable<int> pageIds);

    IMagicPageList GetBreadcrumb(PageState pageState, MagicBreadcrumbSettings? specs = default);

}