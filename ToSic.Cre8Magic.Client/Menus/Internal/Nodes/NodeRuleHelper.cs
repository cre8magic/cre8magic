using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal.Nodes;


internal class NodeRuleHelper(MagicPageFactory pageFactory, IMagicPage current, MagicMenuSettings settings, Log log)
{
    internal Log Log { get; } = log;

    internal MagicPageFactory PageFactory { get; } = pageFactory;

    private IMagicPage Current { get; } = current;

    internal List<IMagicPage> GetRootNodes()
    {
        var l = Log.Fn<List<IMagicPage>>($"{Current.Id}");
        // Give empty list if we shouldn't display it
        if (settings.Display == false)
            return l.Return([], "Display == false, don't show");

        // Case 1: StartPage *, so all top-level entries
        var fallback = MagicMenuSettings.Defaults.Fallback;
        var start = (settings.Start ?? fallback.Start)?.Trim();

        // Case 2: '.' - not yet tested
        var startingPoints = new NodeRuleParser(Log.LogRoot).GetStartNodeRules(start);
        // Case 3: one or more IDs to start from

        var startPages = FindStartPageOfManyRules(startingPoints);

        return l.Return(startPages, startPages.LogPageList());
    }

    internal List<IMagicPage> FindStartPageOfManyRules(List<StartNodeRule> startingPoints)
    {
        var l = Log.Fn<List<IMagicPage>>(string.Join(',', startingPoints.Select(p => p.Id)));
        var result = startingPoints
            .SelectMany(FindStartPagesOfOneRule)
            //.Where(p => p != null)
            .ToList();
        return l.Return(result, result.LogPageList());
    }

    private List<IMagicPage> FindStartPagesOfOneRule(StartNodeRule n)
    {
        var l = Log.Fn<List<IMagicPage>>($"Include hidden pages: {n.Force}; Mode: {n.ModeInfo}");

        // Start by getting all the anchors - the pages to start from, before we know about children or not
        // Three cases: root, current, ...
        var anchorPages = FindInitialAnchorPages(n);

        // only get children if we are not on root (because then we already have the children)
        var getChildren = n.ShowChildren; // && n.ModeInfo != StartMode.Root;

        var result = getChildren
            ? anchorPages.SelectMany(GetPageChildren).ToList()
            : anchorPages;

        return l.Return(result, result.LogPageList());
    }

    private List<IMagicPage> FindInitialAnchorPages(StartNodeRule n)
    {
        var l = Log.Fn<List<IMagicPage>>();
        var source = n.Force
            ? PageFactory.PagesAll().ToList()
            : PageFactory.PagesCurrent().ToList();

        switch (n.ModeInfo)
        {
            case StartMode.PageId:
                return l.Return(source.Where(p => p.Id == n.Id).ToList(), $"Page with id {n.Id}");

            case StartMode.Root:
                return l.Return(source.Where(p => p.MenuLevel == 1).ToList(), "Home/root");

            // Level 0 means current level / current page
            case StartMode.Current when n.Level == 0:
                return l.Return([Current], "Current page");
            // Level 1 means top-level pages. If we don't want the level1 children, we want the top-level items
            // TODO: CHECK WHAT LEVEL Oqtane actually gives us, is 1 the top?

            case StartMode.Current when n is { Level: 1, ShowChildren: false }:
                return l.Return(source.Where(p => p.MenuLevel == 1).ToList(), "Current level 1?");

            case StartMode.Current when n.Level > 0:
                // If coming from the top, level 1 means top level, so skip one less
                var skipDown = n.Level - 1 + (n.ShowChildren ? 1 : 0);
                var (pages, _) = PageFactory.Breadcrumb.Get(new() { Pages = source, Active = Current });
                var fromTop = pages.Skip(skipDown).FirstOrDefault();
                List<IMagicPage> fromTopResult = fromTop == null ? [] : [..fromTop.Children];
                return l.Return(fromTopResult, $"from root to breadcrumb by {skipDown}");

            case StartMode.Current when n.Level < 0:
                // If going up, must change skip to normal
                var skipUp = Math.Abs(n.Level);
                (pages, _) = PageFactory.Breadcrumb.Get(new() { Pages = source, Active = Current });
                var fromCurrent = pages.Reverse().Skip(skipUp).FirstOrDefault();
                List<IMagicPage> result = fromCurrent == null ? [] : [fromCurrent];
                return l.Return(result, $"up the ancestors by {skipUp}");

            case StartMode.Undefined:
            case StartMode.Unknown:
            default:
                return l.Return([], "nothing");
        }
    }


    private List<IMagicPage> GetPageChildren(IMagicPage referencePage)
    {
        var l = Log.Fn<List<IMagicPage>>($"{referencePage.Id}; level");
        var result = referencePage.Children.ToList();
        if (result.Count > 0 || !referencePage.IsHome)
            return l.Return(result, "children - " + result.LogPageList());

        // special case: if it's home, then we may just want to show all level1 pages
        // since "Home" usually doesn't have direct child pages, but behaves as if it does
        result = PageFactory.PagesCurrent()
            .Where(p => p.MenuLevel == 1)
            .ToList();
        return l.Return(result, "children of home - " + result.LogPageList());
    }

}