using System.Net.Security;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal.Nodes;


internal class NodeRulePicker(MagicPageFactory pageFactory, IMagicPage current, Log log)
{
    internal Log Log { get; } = log;

    internal MagicPageFactory PageFactory { get; } = pageFactory;

    private IMagicPage Current { get; } = current;

    internal List<IMagicPage> GetRootNodes(MagicMenuSettings settings)
    {
        var l = Log.Fn<List<IMagicPage>>($"{Current.Id}");
        // Give empty list if we shouldn't display it
        if (settings.Display == false)
            return l.Return([], "Display == false, don't show");

        // Case 1: StartPage *, so all top-level entries
        var start = (settings.Start ?? MagicMenuSettings.Defaults.Fallback.Start)?.Trim();

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
            .ToList();
        return l.Return(result, result.LogPageList());
    }

    internal List<IMagicPage> FindStartPagesOfOneRule(StartNodeRule rule)
    {
        var l = Log.Fn<List<IMagicPage>>($"Include hidden pages: {rule.Force}; Mode: {rule.ModeInfo}");

        // Start by getting all the anchors - the pages to start from, before we know about children or not
        // Three cases: root, current, ...
        var pages = FindInitialPagesRaw(rule);

        // Attach Node Rule to each Page, so further processing knows the depth etc.
        var result = MagicPageFactory.CloneWithNodeRule(pages, rule);

        return l.Return(result, result.LogPageList());
    }


    internal List<IMagicPage> FindInitialPagesRaw(StartNodeRule rule)
    {
        var l = Log.Fn<List<IMagicPage>>($"Rule raw: '{rule.Raw}'; Mode: {rule.ModeInfo} Level: {rule.Level}; Children: {rule.ShowChildren}; Force: {rule.Force}");
        var source = rule.Force
            ? PageFactory.PagesAll().ToList()
            : PageFactory.PagesCurrent().ToList();

        switch (rule.ModeInfo)
        {
            case StartMode.PageId:
                var pagesOfId = source.Where(p => p.Id == rule.Id).ToList();
                var pagesOrChildren = MaybeGetChildren(rule, pagesOfId);
                return l.Return(pagesOrChildren, $"Mode PageId; id: {rule.Id}");

            case StartMode.Root:
                var pagesOfRoot = source.LevelPages(rule.Level);
                pagesOrChildren = pagesOfRoot;// MaybeGetChildren(rule, pagesOfRoot);
                return l.Return(pagesOrChildren, "Mode Root");

            // Level 0 means current level / current page
            case StartMode.Current when rule.Level == 0:
                pagesOrChildren = MaybeGetChildren(rule, [Current]);
                return l.Return(pagesOrChildren, "Mode: Current page");

            case StartMode.Current when rule is { Level: 1, ShowChildren: false }:
                pagesOfRoot = source.LevelPages(1);
                return l.Return(pagesOfRoot, "Current with level=1");

            case StartMode.Current when rule.Level > 0:
                // If coming from the top, level 1 means top level,
                // so skip one less, since we'll always take the children below.
                // If show children is explicit, take one more.
                var skipDown = rule.Level - 1 + (rule.ShowChildren ? 1 : 0);
                var (breadcrumb, _) = PageFactory.Breadcrumb.Get(new() { Pages = source, Active = Current });
                var fromTop = breadcrumb.Skip(skipDown).FirstOrDefault();
                if (fromTop == null)
                    return l.Return([], $"from root to breadcrumb by {skipDown} = empty");

                List<IMagicPage> fromTopResult = [..fromTop.Children];
                pagesOrChildren = MaybeGetChildren(rule, fromTopResult);
                return l.Return(pagesOrChildren, $"from root to breadcrumb by {skipDown}");

            case StartMode.Current when rule.Level < 0:
                // If going up, must change skip to positive number
                var skipUp = Math.Abs(rule.Level);
                l.A($"Mode: Current; Level: {rule.Level}; ShowChildren: {rule.ShowChildren} / {rule.ShowChildren}; {nameof(skipUp)} {skipUp}");

                (breadcrumb, _) = PageFactory.Breadcrumb.Get(new() { Pages = source, Active = Current });
                var fromCurrent = breadcrumb.Reverse().Skip(skipUp).FirstOrDefault();
                if (fromCurrent == null)
                    return l.Return([], "nothing found; return empty");

                // If we came up, and are on the home page,
                // we _only_ all level1 pages if children are requested
                if (fromCurrent.IsHome)
                    return rule.ShowChildren
                        ? l.Return(source.LevelPages(1), "root, children")
                        : l.Return([], "root, no children");

                pagesOrChildren = MaybeGetChildren(rule, [fromCurrent]);
                return l.Return(pagesOrChildren, $"up the ancestors by {skipUp}");

            case StartMode.Undefined:
            case StartMode.Unknown:
            default:
                return l.Return([], "nothing");
        }
    }

    private List<IMagicPage> MaybeGetChildren(StartNodeRule rule, List<IMagicPage> anchorPages)
    {
        var l = Log.Fn<List<IMagicPage>>($"{nameof(rule.ShowChildren)}: {rule.ShowChildren}");

        // only get children if we are not on root (because then we already have the children)
        var result = rule.ShowChildren
            ? anchorPages.SelectMany(GetPageChildren).ToList()
            : anchorPages;

        return l.Return(result, result.LogPageList());
    }


    private List<IMagicPage> GetPageChildren(IMagicPage referencePage)
    {
        var l = Log.Fn<List<IMagicPage>>($"{referencePage.Id}; level");

        var ofPage = referencePage.Children.ToList();
        return l.Return(ofPage, $"children: {ofPage.LogPageList()}");
    }

}