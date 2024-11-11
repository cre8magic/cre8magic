using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal;

internal class MagicPageService() : IMagicPageService
{
    #region Setup and Core Internals

    public PageState PageState => _pageState ?? throw new($"{nameof(PageState)} is not ready, make sure you always call {nameof(Setup)}(PageState) first.");
    private PageState? _pageState;

    public IMagicPageService Setup(PageState pageState)
    {
        _pageState = pageState;
        return this;
    }

    private MagicPageFactory PageFactory => _pageFactory ??= new(PageState);
    private MagicPageFactory? _pageFactory;

    #endregion

    public IEnumerable<IMagicPage> GetAll(bool ignorePermissions = default) =>
        ignorePermissions
            ? PageFactory.All()
            : PageFactory.GetUserPages();

    public IMagicPage GetHome() => PageFactory.Home;

    public IMagicPage GetCurrent() => PageFactory.Current;

    public IMagicPage? GetPage(int pageId) => PageFactory.Get([pageId])?.FirstOrDefault();

    public IMagicPage? GetPage(Page? page) => PageFactory.CreateOrNull(page);


    public IEnumerable<IMagicPage> GetPages(IEnumerable<int> pageIds) => PageFactory.Get(pageIds);

    public IEnumerable<IMagicPage> GetBreadcrumb(MagicBreadcrumbGetSpecsWip? specs = default) => PageFactory.Current.Breadcrumb;

    public IMagicPageList GetMenu() => new MagicMenuTree(PageState);

}