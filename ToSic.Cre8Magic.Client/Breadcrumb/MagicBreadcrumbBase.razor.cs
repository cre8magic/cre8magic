using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumb;

/// <summary>
/// Recommended base class for all breadcrumb components.
/// </summary>
public abstract class MagicBreadcrumbBase: MagicControl
{
    // The home page - never changes during runtime, so we can cache it
    protected IMagicPage HomePage => _homePage ??= PageFactory.Home;
    private IMagicPage? _homePage;

    protected IEnumerable<IMagicPageWithDesignWip> Breadcrumb => GetOrKeepBreadcrumb();

    private IEnumerable<IMagicPageWithDesignWip> GetOrKeepBreadcrumb()
    {
            // Reset cache if the page has changed
            if (_lastPageId != PageState.Page.PageId)
            {
                _breadcrumbs = null;
                _lastPageId = PageState.Page.PageId;
            }

            // Return cached or new breadcrumbs
            return _breadcrumbs ??= PageFactory.Breadcrumb.Get();
    }

    private int? _lastPageId;
    private IEnumerable<IMagicPageWithDesignWip>? _breadcrumbs;
}