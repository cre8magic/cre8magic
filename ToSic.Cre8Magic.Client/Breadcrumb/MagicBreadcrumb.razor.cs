using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Breadcrumb;

public abstract class MagicBreadcrumb: MagicControl
{
    // The home page - never changes during runtime, so we can cache it
    protected IMagicPage HomePage => _homePage ??= PageFactory.Home;
    private IMagicPage? _homePage;

    protected IEnumerable<IMagicPage> Breadcrumbs
    {
        get
        {
            // Reset cache if the page has changed
            if (_lastPageId != PageState.Page.PageId)
                _breadcrumbs = null;
            // Remember the current page
            _lastPageId = PageState.Page.PageId;

            // Return cached or new breadcrumbs
            return _breadcrumbs ??= PageFactory.Breadcrumb.Get();
        }
    }

    private int? _lastPageId;
    private IEnumerable<IMagicPage>? _breadcrumbs;
}