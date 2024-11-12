using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Pages.Internal.Menu;

/// <summary>
/// The builder for all sub-items of a magic menu.
///
/// Not used for the root though...
/// </summary>
/// <param name="pageFactory"></param>
internal class MagicMenuFactory(MagicPageFactory pageFactory, MagicMenuGetSpecsWip specsForDebug, Func<int> getMaxDepth) : MagicPagesFactoryBase(pageFactory)
{
    public List<string> DebugMessages => specsForDebug.DebugMessages;

    public void Set(MagicMenuSettings? settings) => _settings = settings;

    /// <summary>
    /// Settings - on first access takes the one given, or creates a default.
    /// </summary>
    public MagicMenuSettings SettingsTyped => _settings ??= MagicMenuSettings.Defaults.Fallback;
    private MagicMenuSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => new MagicMenuDesigner(this, SettingsTyped);


    /// <summary>
    /// Retrieve the children the first time it's needed.
    /// </summary>
    /// <returns></returns>
    public override List<IMagicPageWithDesignWip> GetChildren(IMagicPage page)
    {
        var l = Log.Fn<List<IMagicPageWithDesignWip>>($"{nameof(page.MenuLevel)}: {page.MenuLevel}");
        var maxDepth = getMaxDepth(); // getTree().MaxDepth;// ?? SettingsTyped.Depth ?? MagicMenuSettings.LevelDepthFallback;
        var levelsRemaining = maxDepth - (page.MenuLevel - 1 /* Level is 1 based, so -1 */);
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var children = base.GetChildren(page);

        return l.Return(children, $"{children.Count}");
    }

}