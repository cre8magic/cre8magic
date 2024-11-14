using ToSic.Cre8magic.Menus.Settings;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Helper to find various pages based on rules such as
/// * "27" - the page 27
/// * "27,28" - the pages 27 and 28
/// * "*" - the top-level pages
/// * "." - the current page
/// </summary>
internal class NodeRuleHelper(MagicPageFactory pageFactory, IMagicPage current, Log log)
{
    public const char PageForced = '!';

    internal Log Log { get; } = log;

    internal MagicPageFactory PageFactory { get; } = pageFactory;

    private IMagicPage Current { get; } = current;

    internal List<IMagicPage> GetRootPages(MagicMenuSettings settings)
    {
        var l = Log.Fn<List<IMagicPage>>($"{Current.Id}");
        // Give empty list if we shouldn't display it
        if (settings.Display == false)
            return l.Return([], "Display == false, don't show");

        // Case 1: StartPage *, so all top-level entries
        var fallback = MagicMenuSettings.Defaults.Fallback;
        var start = (settings.Start ?? fallback.Start)?.Trim();

        // Case 2: '.' - not yet tested
        var startLevel = settings.Level ?? settings.Level ?? MagicMenuSettings.StartLevelFallback;
        var getChildren = settings.Children ?? settings.Children ?? MagicMenuSettings.ChildrenFallback;
        var startingPoints = GetStartNodeRules(start, startLevel, getChildren);
        // Case 3: one or more IDs to start from

        var startPages = FindStartPageOfManyRules(startingPoints);

        return l.Return(startPages, startPages.LogPageList());
    }

    internal List<IMagicPage> FindStartPageOfManyRules(StartNodeRule[] startingPoints)
    {
        var l = Log.Fn<List<IMagicPage>>(string.Join(',', startingPoints.Select(p => p.Id)));
        var result = startingPoints
            .SelectMany(FindStartPagesOfOneRule)
            .Where(p => p != null)
            .ToList();
        return l.Return(result, result.LogPageList());
    }

    private List<IMagicPage> FindStartPagesOfOneRule(StartNodeRule n)
    {
        var l = Log.Fn<List<IMagicPage>>($"Include hidden pages: {n.Force}; Mode: {n.ModeInfo}");

        // Start by getting all the anchors - the pages to start from, before we know about children or not
        // Three cases: root, current, ...
        var anchorPages = FindInitialAnchorPages(n);

        var result = n.ShowChildren
            ? anchorPages.SelectMany(p => GetRelatedPagesByLevel(p, 1)).ToList()
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
                var skipDown = n.Level - 1;
                var fromTop = PageFactory.Breadcrumb.Get(new() { Pages = source, Active = Current }).Skip(skipDown).FirstOrDefault();
                List<IMagicPage> fromTopResult = fromTop == null ? [] : [fromTop];
                return l.Return(fromTopResult, $"from root to breadcrumb by {skipDown}");
            case StartMode.Current when n.Level < 0:
                // If going up, must change skip to normal
                var skipUp = Math.Abs(n.Level);
                var fromCurrent = PageFactory.Breadcrumb.Get(new() { Pages = source, Active = Current }).ToList().Skip(skipUp).FirstOrDefault();
                List<IMagicPage> result = fromCurrent == null ? [] : [fromCurrent];
                return l.Return(result, $"up the ancestors by {skipUp}");
            case StartMode.Undefined:
            case StartMode.Unknown:
            default:
                return l.Return([], "nothing");
        }
    }


    private List<IMagicPage> GetRelatedPagesByLevel(IMagicPage referencePage, int level)
    {
        var l = Log.Fn<List<IMagicPage>>($"{referencePage.Id}; {level}");
        List<IMagicPage> result;
        switch (level)
        {
            case -1:
                result = PageFactory.ChildrenOf(referencePage.ParentId ?? 0);
                return l.Return(result, "siblings - " + result.LogPageList());
            case 0:
                result = [referencePage];
                return l.Return(result, "current page - " + result.LogPageList());
            case 1:
                result = PageFactory.ChildrenOf(referencePage.Id);
                return l.Return(result, "children - " + result.LogPageList());
            case > 1:
                return l.Return([PageFactory.ErrPage(0, "Error: Create menu from current page but level > 1")], "err");
            default:
                return l.Return(
                    [PageFactory.ErrPage(0, "Error: Create menu from current page but level < -1, not yet implemented")], "err");
        }
    }

    private StartNodeRule[] GetStartNodeRules(string? value, int level, bool showChildren)
    {
        var l = Log.Fn<StartNodeRule[]>($"{nameof(value)}: '{value}'; {nameof(level)}: {level}; {nameof(showChildren)}: {showChildren}");

        if (!value.HasText()) return l.Return([], "no value, empty list");
        var parts = value.Split(',');
        var result = parts
            .Select(fromNode =>
            {
                fromNode = fromNode.Trim();
                if (!fromNode.HasText()) return null;
                var important = fromNode.EndsWith(PageForced);
                if (important) fromNode = fromNode.TrimEnd(PageForced);
                fromNode = fromNode.Trim();
                int.TryParse(fromNode, out var id);
                return new StartNodeRule { Id = id, From = fromNode, Force = important, ShowChildren = showChildren, Level = level };
            })
            .Where(n => n != null)
            .ToArray() as StartNodeRule[];
        return l.ReturnAndKeepData(result, result.Length.ToString());
    }
}