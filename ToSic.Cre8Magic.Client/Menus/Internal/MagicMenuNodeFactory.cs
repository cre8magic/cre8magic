using ToSic.Cre8magic.Internal.Logging;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// The builder for all sub-items of a magic menu.
///
/// Not used for the root though...
/// </summary>
internal class MagicMenuNodeFactory(MagicMenuWorkContext workContext) 
    : MagicPagesFactoryBase(workContext)
{
    protected override IMagicPageTailor PageTailor() =>
        workContext.Tailor ?? new MagicMenuTailor(workContext);

    /// <summary>
    /// Retrieve the children the first time it's needed.
    /// </summary>
    /// <returns></returns>
    public override List<IMagicPage> ChildrenOf(IMagicPage page)
    {
        var l = Log.Fn<List<IMagicPage>>($"{nameof(page.MenuLevel)}: {page.MenuLevel}");

        // On the first level, we should construct the base list
        var magicPage = page as MagicPage
                        ?? throw new NotSupportedException($"The {nameof(page)} is not a {nameof(MagicPage)}");
        
        if (magicPage.IsVirtualRoot)
            return l.Return(GetRootPages(workContext, this), "Root");


        var levelsRemaining = (magicPage.NodeRule?.Depth ?? 0) - (page.MenuLevel /*- 1*/ /* Level is 1 based, so -1 */);
        if (levelsRemaining <= 0)
            return l.Return([], "remaining levels 0 - return empty");

        var children = base.ChildrenOf(page);

        return l.Return(children, $"{children.Count}");
    }



    private static List<IMagicPage> GetRootPages(MagicMenuWorkContext workContext, MagicMenuNodeFactory nodeFactory)
    {
        var log = workContext.LogRoot.GetLog("get-root");

        var pageFactory = workContext.PageFactory;
        var settings = workContext.Settings;

        // Add break-point for debugging during development
        if (pageFactory.PageState.IsDebug())
            pageFactory.PageState.DoNothing();

        var l = log.Fn<List<IMagicPage>>("Root");
        l.A($"Start with PageState for Page:{pageFactory.Current.Id}; Level:1");

        var rootPages = new PagePicker.PagePicker(pageFactory, pageFactory.Current, log).GetRootNodes(settings);
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(IMagicPage (child) => new MagicPage(child.RawPage, pageFactory, nodeFactory)
            {
                MenuLevel = 1, /* todo: should probably be 1 */
                NodeRule = ((MagicPage)child).NodeRule,
            })
            .ToList();
        return l.Return(children, $"{children.Count}");
    }
}