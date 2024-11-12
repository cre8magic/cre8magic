namespace ToSic.Cre8magic.Pages.Internal.Breadcrumb;

internal class MagicBreadcrumbFactoryRoot(MagicPageFactory pageFactory)
{
    private MagicBreadcrumbFactory BreadcrumbFactory(MagicBreadcrumbGetSpecsWip specs)
    {
        var setHelper = new MagicBreadcrumbFactory(pageFactory);
        if (specs.Settings != null)
            setHelper.Set(specs.Settings);
        if (specs.Designer != null)
            setHelper.Set(specs.Designer);
        return setHelper;
    }

    internal IMagicPageList Get(MagicBreadcrumbGetSpecsWip? specs = default)
    {
        specs ??= new();
        var factory = BreadcrumbFactory(specs);
        var list = Get(
            specs,
            magicPage => new MagicPageWithDesign(pageFactory, factory, magicPage)
        );
        return new MagicPageList(pageFactory, factory, list);
    }

    private IEnumerable<TPage> Get<TPage>(MagicBreadcrumbGetSpecsWip? specs, Func<IMagicPage, TPage> generator)
    {
        specs ??= new();
        var endPage = specs.Current ?? pageFactory.Current;
        // Create a new list with the current page
        var list = new List<TPage>();

        if (specs.WithCurrent)
            list.Add(generator(endPage));

        // If we are on home, exit now.
        var homeId = pageFactory.Home.Id;
        if (homeId == endPage.Id)
            return list;

        // Technically home is not in the breadcrumb, it's usually just the first page in the list
        if (specs.WithHome)
            list.Insert(0, generator(pageFactory.Home));

        // determine if we restrict the output list
        // Note that as of 2024-11-10 it has not been tested.
        var restrictions = specs.Pages?.Select(p => p.Id).ToHashSet();

        //// Find first parent page
        var parentPage = endPage.Parent;

        // Loop through all parent pages until we reach the home page
        while (parentPage != null && homeId != parentPage.Id && list.Count <= specs.MaxDepth)
        {
            // Check if not in the list of restrictions
            if (restrictions != null && !restrictions.Contains(parentPage.Id))
                break;

            // Add to list
            list.Insert(1, generator(parentPage));
            // Find next parent
            parentPage = parentPage.Parent;
        }

        if (specs.Reverse)
            list.Reverse();

        return list;
    }
}