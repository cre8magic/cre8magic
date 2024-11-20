using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbBuilder(MagicPageFactory pageFactory)
{
    internal IMagicPageList Get(MagicBreadcrumbSettings? settings = default)
    {
        settings ??= MagicBreadcrumbSettings.Defaults.Fallback;
        var context = new ContextWip<MagicBreadcrumbSettings, IMagicPageDesigner>(
            settings,
            settings.Designer,
            pageFactory,
            null  // TODO: SHOULD provide AllSettings or whatever will replace it, so we can get the Page tokens
        );
        var factory = new MagicBreadcrumbNodeFactory(context);
        var list = Get(
            settings,
            magicPage => new MagicPageWithDesign(pageFactory, factory, magicPage)
        );
        return new MagicPageList(pageFactory, factory, list);
    }

    private IEnumerable<TPage> Get<TPage>(MagicBreadcrumbSettings settings, Func<IMagicPage, TPage> generator)
    {
        // Check if we have a specified current page
        var endPage = settings.Active;

        // If not, and we have a CurrentId, try that.
        // If there is no match, exit.
        if (endPage == null && settings.ActiveId != null)
        {
            endPage = pageFactory.GetOrNull(settings.ActiveId);
            if (endPage == null)
                return new List<TPage>();
        }

        // In case we didn't have a hit, use the current page
        endPage ??= pageFactory.Current;

        // Create a new list with the current page
        var list = new List<TPage>();

        if (settings.WithActive)
            list.Add(generator(endPage));

        // If we are on home, exit now.
        var homeId = pageFactory.Home.Id;
        if (homeId == endPage.Id)
            return list;

        // Technically home is not in the breadcrumb, it's usually just the first page in the list
        if (settings.WithHome)
            list.Insert(0, generator(pageFactory.Home));

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
            list.Insert(1, generator(parentPage));
            // Find next parent
            parentPage = parentPage.Parent;
        }

        if (settings.Reverse)
            list.Reverse();

        return list;
    }
}