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
    public MagicMenuSettingsData SettingsTyped => _settings ??= context.Settings ?? MagicMenuSettingsData.Defaults.Fallback;
    private MagicMenuSettingsData? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => new MagicMenuDesigner(context);

    public int MaxDepth => _maxDepth ??= context.Settings.Depth ?? MagicMenuSettingsData.Defaults.Fallback.Depth!.Value;
    private int? _maxDepth;

    /// <summary>
    /// Retrieve the children the first time it's needed.
    /// </summary>
    /// <returns></returns>
    public override List<IMagicPageWithDesignWip> GetChildren(IMagicPage page)
    {
        var l = Log.Fn<List<IMagicPageWithDesignWip>>($"{nameof(page.MenuLevel)}: {page.MenuLevel}");
        var levelsRemaining = MaxDepth - (page.MenuLevel - 1 /* Level is 1 based, so -1 */);
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var children = base.GetChildren(page);

        return l.Return(children, $"{children.Count}");
    }

}