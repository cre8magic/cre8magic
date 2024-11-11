using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Client.Pages;

// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

public interface IMagicPageService
{
    /// <summary>
    /// The page state - must be initialized before using the service.
    /// </summary>
    /// <remarks>
    /// Will throw an error if accessed before initializing.
    /// </remarks>
    PageState PageState { get; }

    IMagicPageService Setup(PageState pageState);

    IEnumerable<IMagicPage> GetAll(bool ignorePermissions = default);

    IMagicPage GetHome();

    IMagicPage GetCurrent();

    IMagicPage? GetPage(int pageId);

    IMagicPage? GetPage(Page? page);

    IEnumerable<IMagicPage> GetPages(IEnumerable<int> pageIds);

    IMagicPageList GetBreadcrumb(MagicBreadcrumbGetSpecsWip? specs = default);

    IMagicPageList GetMenu(MagicMenuGetSpecsWip? specs = default);
}