using Oqtane.UI;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal.Menu;

internal class MagicMenuFactoryRoot
{
    public MagicMenuFactoryRoot(PageState pageState, MagicMenuGetSpecsWip specs)
    {
        Specs = specs;
        PageFactory = new(pageState, specs.Pages);
        Factory = new(PageFactory, specs, () => MaxDepth);
        Factory.Set(specs.MagicSettings);
        Factory.Set(specs.Designer);
        Factory.Set(specs.Settings);
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



    internal MagicMenuGetSpecsWip Specs { get; }

    internal MagicMenuFactory Factory { get; }

    public int MaxDepth => _maxDepth ??= Specs.Depth ?? Specs.Settings?.Depth ?? MagicMenuSettings.LevelDepthFallback;
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