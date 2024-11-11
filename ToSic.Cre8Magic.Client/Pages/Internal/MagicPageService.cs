using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Client.Pages.Internal.Menu;
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
            ? PageFactory.PagesAll()
            : PageFactory.PagesUser();

    public IMagicPage GetHome() => PageFactory.Home;

    public IMagicPage GetCurrent() => PageFactory.Current;

    public IMagicPage? GetPage(int pageId) => PageFactory.GetOrNull(pageId);

    public IMagicPage? GetPage(Page? page) => PageFactory.CreateOrNull(page);


    public IEnumerable<IMagicPage> GetPages(IEnumerable<int> pageIds) => PageFactory.Get(pageIds);

    public IMagicPageList GetBreadcrumb(MagicBreadcrumbGetSpecsWip? specs = default) =>
        PageFactory.Breadcrumb.Get(specs);

    public IMagicPageList GetMenu(MagicMenuGetSpecsWip? specs = default)
    {
        specs ??= new();
        //var PageFactory = new MagicPageFactory(PageState, specs.Pages);
        //var SetHelper = new MagicMenuPageSetHelper(PageFactory, this);
        //SetHelper.Set(specs.MagicSettings);
        //SetHelper.Set(specs.Designer);
        //SetHelper.Set(specs.Settings);

        var rootBuilder = new MagicMenuRootBuilder(PageState, specs);

        var list = new MagicPageList(PageFactory, rootBuilder.SubSetHelper, rootBuilder.GetChildren());

        //return list;
        return new MagicMenuTree(PageState, specs);
    }

}