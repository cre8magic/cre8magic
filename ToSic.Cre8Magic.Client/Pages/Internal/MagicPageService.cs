using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumb.Settings;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Pages.Internal.Menu;

namespace ToSic.Cre8magic.Pages.Internal;

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

    public IMagicPage GetHome() =>
        PageFactory.Home;

    public IMagicPage GetCurrent() =>
        PageFactory.Current;

    public IMagicPage? GetPage(int pageId) =>
        PageFactory.GetOrNull(pageId);

    public IMagicPage? GetPage(Page? page) =>
        PageFactory.CreateOrNull(page);


    public IEnumerable<IMagicPage> GetPages(IEnumerable<int> pageIds) =>
        PageFactory.Get(pageIds);

    public IMagicPageList GetBreadcrumb(MagicBreadcrumbSettings? specs = default) =>
        PageFactory.Breadcrumb.Get(specs);

    public IMagicPageList GetMenu(MagicMenuSettings? specs = default) =>
        GetMenuInternal(specs, null);

    public IMagicPageList GetMenuInternal(MagicMenuSettings? specs, List<string>? debugMessages)
    {
        specs ??= new();
        var rootBuilder = new MagicMenuFactoryRoot(PageState, specs, debugMessages);
        var list = new MagicPageList(PageFactory, rootBuilder.Factory, rootBuilder.GetChildren());
        return list;
    }
}