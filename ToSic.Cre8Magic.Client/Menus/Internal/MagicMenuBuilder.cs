using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

// TODO: probably merge into the MagicMenuService
internal class MagicMenuBuilder
{
    public MagicMenuBuilder(MagicMenuContextWip context)
    {
        Context = context;
        Settings = context.Settings;
        NodeFactory = new(context, () => MaxDepth);
        Log = context.LogRoot.GetLog("root");
        Log.A($"Start with PageState for Page:{PageFactory.Current.Id}; Level:1");

    }

    internal MagicMenuContextWip Context;

    internal MagicPageFactory PageFactory => Context.PageFactory;

    private Log Log { get; }
    
    internal MagicMenuSettingsData Settings { get; }

    internal MagicMenuNodeFactory NodeFactory { get; }

    public int MaxDepth => _maxDepth ??= Settings.Depth ?? Settings.Depth ?? MagicMenuSettingsData.Defaults.Fallback.Depth!.Value;
    private int? _maxDepth;

    public List<IMagicPageWithDesignWip> GetChildren()
    {
        // Add break-point for debugging during development
        if (PageFactory.PageState.IsDebug()) PageFactory.PageState.DoNothing();

        var l = Log.Fn<List<IMagicPageWithDesignWip>>("Root");
        var levelsRemaining = MaxDepth;
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var rootPages = new NodeRuleHelper(PageFactory, PageFactory.Current, Settings, Log).GetRootPages();
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(child => new MagicPageWithDesign(PageFactory, NodeFactory, child, 2 /* todo: should probably be 1 */) as IMagicPageWithDesignWip)
            .ToList();
        return l.Return(children, $"{children.Count}");
    }

}