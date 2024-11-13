using Oqtane.UI;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Pages.Internal.Menu;

internal class MagicMenuFactoryRoot
{
    public MagicMenuFactoryRoot(PageState pageState, MagicMenuSettings specs, List<string>? debugMessages)
    {
        Specs = specs;
        PageFactory = new(pageState, specs.Pages);
        Factory = new(PageFactory, specs, debugMessages, () => MaxDepth);
        Factory.Set(specs.AllSettings);
        Factory.Set(specs.Designer);
        Factory.Set(specs/*.Settings*/);
        Log = new LogRoot().GetLog("root");
        //Log = SetHelper.LogRoot.GetLog("Root");
        Log.A($"Start with PageState for Page:{PageFactory.Current.Id}; Level:1");

    }

    internal MagicPageFactory PageFactory { get; }

    private Log Log { get; }

    /// <summary>
    /// Settings - on first access takes the one given, or creates a default.
    /// </summary>
    public MagicMenuSettings SettingsTyped => _settings ??= MagicMenuSettings.Defaults.Fallback;
    private MagicMenuSettings? _settings;

    public IMagicPageSetSettings Settings => SettingsTyped;



    internal MagicMenuSettings Specs { get; }

    internal MagicMenuFactory Factory { get; }

    public int MaxDepth => _maxDepth ??= Specs.Depth ?? Specs.Depth ?? MagicMenuSettings.LevelDepthFallback;
    private int? _maxDepth;

    public List<IMagicPageWithDesignWip> GetChildren()
    {
        var l = Log.Fn<List<IMagicPageWithDesignWip>>("Root");
        var settings = (MagicMenuSettings)Factory.Settings;
        var levelsRemaining = MaxDepth;
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var rootPages = new NodeRuleHelper(PageFactory, PageFactory.Current, settings, Log).GetRootPages(Specs);
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(child => new MagicPageWithDesign(PageFactory, Factory, child, 2 /* todo: should probably be 1 */) as IMagicPageWithDesignWip)
            .ToList();
        return l.Return(children, $"{children.Count}");
    }

}