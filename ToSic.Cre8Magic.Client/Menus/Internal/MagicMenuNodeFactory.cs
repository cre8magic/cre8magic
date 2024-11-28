using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// The builder for all sub-items of a magic menu.
///
/// Not used for the root though...
/// </summary>
internal class MagicMenuNodeFactory(MagicMenuContextWip context) 
    : MagicPagesFactoryBase(context)
{
    /// <summary>
    /// Settings - on first access takes the one given, or creates a default.
    /// </summary>
    public MagicMenuSettings SettingsTyped => _settings ??= context.Settings ?? MagicMenuSettings.Defaults.Fallback;
    private MagicMenuSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => new MagicMenuDesigner(context);

    public int MaxDepth => _maxDepth ??= context.Settings.Depth ?? MagicMenuSettings.Defaults.Fallback.Depth!.Value;
    private int? _maxDepth;

    /// <summary>
    /// Retrieve the children the first time it's needed.
    /// </summary>
    /// <returns></returns>
    public override List<IMagicPage> ChildrenOf(IMagicPage page)
    {
        var l = Log.Fn<List<IMagicPage>>($"{nameof(page.MenuLevel)}: {page.MenuLevel}");

        // On the first level, we should construct the base list
        if ((page as MagicPage)?.IsVirtualRoot == true)
            return l.Return(GetRootPages(context, this), "Root");


        var levelsRemaining = MaxDepth - (page.MenuLevel - 1 /* Level is 1 based, so -1 */);
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var children = base.ChildrenOf(page);

        return l.Return(children, $"{children.Count}");
    }



    private static List<IMagicPage> GetRootPages(MagicMenuContextWip context, MagicMenuNodeFactory nodeFactory)
    {
        var log = context.LogRoot.GetLog("get-root");

        var pageFactory = context.PageFactory;
        var settings = context.Settings;

        // Add break-point for debugging during development
        if (pageFactory.PageState.IsDebug())
            pageFactory.PageState.DoNothing();

        var l = log.Fn<List<IMagicPage>>("Root");
        l.A($"Start with PageState for Page:{pageFactory.Current.Id}; Level:1");

        var levelsRemaining = nodeFactory.MaxDepth;
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var rootPages = new NodeRuleHelper(pageFactory, pageFactory.Current, settings, log).GetRootNodes();
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(IMagicPage (child) => new MagicPage(child.OqtanePage, pageFactory, nodeFactory)
            {
                MenuLevel = 2 /* todo: should probably be 1 */
            })
            .ToList();
        return l.Return(children, $"{children.Count}");
    }
}