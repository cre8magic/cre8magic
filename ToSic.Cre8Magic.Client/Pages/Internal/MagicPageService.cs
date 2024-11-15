using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumb.Settings;

namespace ToSic.Cre8magic.Pages.Internal;

internal class MagicPageService() : IMagicPageService
{
    public IEnumerable<IMagicPage> GetAll(PageState pageState, bool ignorePermissions = default) =>
        ignorePermissions
            ? GetFactory(pageState).PagesAll()
            : GetFactory(pageState).PagesUser();

    public IMagicPage GetHome(PageState pageState) =>
        GetFactory(pageState).Home;

    public IMagicPage GetCurrent(PageState pageState) =>
        GetFactory(pageState).Current;

    public IMagicPage? GetPage(PageState pageState, int pageId) =>
        GetFactory(pageState).GetOrNull(pageId);

    public IMagicPage? GetPage(PageState pageState, Page? page) =>
        GetFactory(pageState).CreateOrNull(page);


    public IEnumerable<IMagicPage> GetPages(PageState pageState, IEnumerable<int> pageIds) =>
        GetFactory(pageState).Get(pageIds);

    private MagicPageFactory GetFactory(PageState pageState) => new(pageState);

    //public IMagicPageList GetBreadcrumb(MagicBreadcrumbSettings? specs = default) =>
    //    GetBreadcrumb(PageFactory, specs);

    public IMagicPageList GetBreadcrumb(PageState pageState, MagicBreadcrumbSettings? specs = default) =>
        GetBreadcrumb(GetFactory(pageState), specs);

    private IMagicPageList GetBreadcrumb(MagicPageFactory pageFactory, MagicBreadcrumbSettings? specs = default) =>
        pageFactory.Breadcrumb.Get(specs);

}