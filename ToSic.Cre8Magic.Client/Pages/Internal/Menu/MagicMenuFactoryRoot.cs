using Oqtane.UI;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Pages.Internal.Menu;

internal class MagicMenuFactoryRoot
{
    public MagicMenuFactoryRoot(PageState pageState, MagicMenuSettings settings, List<string>? debugMessages)
    {
        Settings = settings;
        PageFactory = new(pageState, settings.Pages);
        Factory = new(PageFactory, settings, debugMessages, () => MaxDepth);
        Factory.Set(settings.AllSettings);
        Factory.Set(settings.Designer);
        Factory.Set(settings);
        Log = new LogRoot().GetLog("root");
        //Log = SetHelper.LogRoot.GetLog("Root");
        Log.A($"Start with PageState for Page:{PageFactory.Current.Id}; Level:1");

    }

    internal MagicPageFactory PageFactory { get; }

    private Log Log { get; }
    
    internal MagicMenuSettings Settings { get; }

    internal MagicMenuFactory Factory { get; }

    public int MaxDepth => _maxDepth ??= Settings.Depth ?? Settings.Depth ?? MagicMenuSettings.Defaults.Fallback.Depth!.Value;
    private int? _maxDepth;

    public List<IMagicPageWithDesignWip> GetChildren()
    {
        // Add break-point for debugging during development
        if (PageFactory.PageState.IsDebug()) PageFactory.PageState.DoNothing();

        var l = Log.Fn<List<IMagicPageWithDesignWip>>("Root");
        var levelsRemaining = MaxDepth;
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var rootPages = new NodeRuleHelper(PageFactory, PageFactory.Current, Log).GetRootPages(Settings);
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(child => new MagicPageWithDesign(PageFactory, Factory, child, 2 /* todo: should probably be 1 */) as IMagicPageWithDesignWip)
            .ToList();
        return l.Return(children, $"{children.Count}");
    }

}