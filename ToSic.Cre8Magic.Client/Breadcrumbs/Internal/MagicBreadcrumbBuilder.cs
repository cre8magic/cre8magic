using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbBuilder(WorkContext workContext)
{
    private readonly MagicPageFactory _pageFactory = workContext.PageFactory;

    internal (IEnumerable<IMagicPage> Pages, IMagicPageChildrenFactory ChildrenFactory) Get(MagicBreadcrumbSettings? settings = default)
    {
        settings ??= MagicBreadcrumbSettings.Defaults.Fallback;
        var context = new WorkContext<MagicBreadcrumbSettings, IMagicPageDesigner>
        {
            Designer = settings.Designer,
            LogRoot = workContext.LogRoot,
            PageFactory = _pageFactory,
            TokenEngine = workContext.TokenEngine,
            Settings = settings,
        };

        var childrenFactory = new MagicBreadcrumbNodeFactory(context);
        var list = BuildBreadcrumbs(
            settings,
            magicPage => new MagicPage(magicPage.OqtanePage, _pageFactory, childrenFactory)
        );
        return (list, childrenFactory);
    }

    private IEnumerable<TPage> BuildBreadcrumbs<TPage>(MagicBreadcrumbSettings settings, Func<IMagicPage, TPage> generator)
    {
        // Check if we have a specified current page
        var endPage = settings.Active;

        // If not, and we have a CurrentId, try that.
        // If there is no match, exit.
        if (endPage == null && settings.ActiveId != null)
        {
            endPage = _pageFactory.GetOrNull(settings.ActiveId);
            if (endPage == null)
                return new List<TPage>();
        }

        // In case we didn't have a hit, use the current page
        endPage ??= _pageFactory.Current;

        // Create a new list with the current page
        var list = new List<TPage>();

        if (settings.WithActiveSafe)
            list.Add(generator(endPage));

        // If we are on home, exit now.
        var homeId = _pageFactory.Home.Id;
        if (homeId == endPage.Id)
            return list;

        // determine if we restrict the output list
        // Note that as of 2024-11-10 it has not been tested.
        var restrictions = settings.Pages?.Select(p => p.Id).ToHashSet();

        //// Find first parent page
        var parentPage = endPage.Parent;

        // Loop through all parent pages until we reach the home page
        while (parentPage != null && homeId != parentPage.Id && list.Count <= settings.MaxDepth)
        {
            // Check if not in the list of restrictions
            if (restrictions != null && !restrictions.Contains(parentPage.Id))
                break;

            // Add to list
            list.Add(generator(parentPage));
            // Find next parent
            parentPage = parentPage.Parent;
        }

        // Technically home is not in the breadcrumb, it's usually just the first page in the list
        if (settings.WithHomeSafe)
            list.Add(generator(_pageFactory.Home));

        // Reverse is a setting where the developer will think the breadcrumb is reversed
        // but since we're creating the list in reverse, we must do the opposite check
        if (!settings.ReverseSafe)
            list.Reverse();

        return list;
    }
}