using ToSic.Cre8magic.Menus.Settings;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// The builder for all sub-items of a magic menu.
///
/// Not used for the root though...
/// </summary>
internal class MagicMenuNodeFactory(ContextWip<MagicMenuSettings, IMagicPageDesigner> context, Func<int> getMaxDepth) 
    : MagicPagesFactoryBase(context)
{
    /// <summary>
    /// Settings - on first access takes the one given, or creates a default.
    /// </summary>
    public MagicMenuSettings SettingsTyped => _settings ??= context.Settings ?? MagicMenuSettings.Defaults.Fallback;
    private MagicMenuSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => new MagicMenuDesigner(context);


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