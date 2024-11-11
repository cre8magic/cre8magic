using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal;

internal class MagicBreadcrumbFactory(MagicPageFactory pageFactory)
{
    internal IEnumerable<IMagicPage> Get(IMagicPage? page = null)
        => Get(null, ((_, magicPage) => magicPage), page);

    internal IEnumerable<IMagicPage> Get(IList<IMagicPage> pages, IMagicPage? page)
        => Get(new() { Pages = pages }, ((_, magicPage) => magicPage), page);

    internal IEnumerable<TPage> Get<TPage>(MagicBreadcrumbGetSpecsWip? specs, Func<MagicPageFactory, IMagicPage, TPage> generator, IMagicPage? page = null)
    {
        specs ??= new();
        var endPage = page ?? pageFactory.Current;
        // Create a new list with the current page
        var list = new List<TPage>();

        if (specs.WithCurrent)
            list.Add(generator(pageFactory, endPage));

        // If we are on home, exit now.
        var homeId = pageFactory.Home.Id;
        if (homeId == endPage.Id)
            return list;

        // Technically home is not in the breadcrumb, it's usually just the first page in the list
        if (specs.WithHome)
            list.Insert(0, generator(pageFactory, pageFactory.Home));

        // determine if we restrict the output list
        // Note that as of 2024-11-10 it has not been tested.
        var restrictions = specs.Pages?.Select(p => p.Id).ToHashSet();

        //// Find first parent page
        //var oqtPages = (specs.Pages ?? PageState.Pages).ToList();
        //var parentPage = oqtPages.FirstOrDefault(p => p.PageId == endPage.ParentId);
        var parentPage = endPage.Parent;

        // Loop through all parent pages until we reach the home page
        while (parentPage != null && homeId != parentPage.Id && list.Count <= specs.MaxDepth)
        {
            // Check if not in the list of restrictions
            if (restrictions != null && !restrictions.Contains(parentPage.Id))
                break;

            // Add to list
            list.Insert(1, generator(pageFactory, parentPage));
            // Find next parent
            parentPage = parentPage.Parent; // oqtPages.FirstOrDefault(p => p.PageId == parentPage.ParentId);
        }

        if (specs.Reverse)
            list.Reverse();

        return list;
    }
}