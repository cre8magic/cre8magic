using Oqtane.UI;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal;

internal class MagicPageService() : IMagicPageService
{
    #region Setup and Core Internals

    public PageState PageState => _pageState ?? throw new($"{nameof(PageState)} is not ready, make sure you always call {nameof(AddState)}(PageState) first.");
    private PageState? _pageState;

    public IMagicPageService AddState(PageState pageState)
    {
        _pageState = pageState;
        return this;
    }

    private MagicPageFactory PageFactory => _pageFactory ??= new(PageState);
    private MagicPageFactory? _pageFactory;

    #endregion

    public IEnumerable<IMagicPage> All => PageFactory.All();

    public IEnumerable<IMagicPage> UserAll => null;

    public IMagicPage Home => PageFactory.Home;

    public IMagicPage Current => PageFactory.Current;

    public IMagicPage? Get(int pageId) => PageFactory.Get([pageId])?.FirstOrDefault();

    public IEnumerable<IMagicPage> Get(IEnumerable<int> pageIds) => PageFactory.Get(pageIds);

    public IMagicPageList Menu => new MagicMenuTree(PageState);

}