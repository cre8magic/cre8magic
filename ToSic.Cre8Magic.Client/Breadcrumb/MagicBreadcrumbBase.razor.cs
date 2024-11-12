using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Breadcrumb;

/// <summary>
/// Recommended base class for all breadcrumb components.
/// </summary>
public abstract class MagicBreadcrumbBase: MagicControl
{
    // The home page - never changes during runtime, so we can cache it
    protected IMagicPage HomePage => _homePage ??= PageFactory.Home;
    private IMagicPage? _homePage;

    protected IEnumerable<IMagicPageWithDesignWip> Breadcrumb => _breadcrumbs.Get(
        () => (PageFactory.Breadcrumb.Get(), PageState.Page.PageId),
        (_, i) => i == PageState.Page.PageId
    );
    private readonly GetKeep<IEnumerable<IMagicPageWithDesignWip>, int?> _breadcrumbs = new();

}