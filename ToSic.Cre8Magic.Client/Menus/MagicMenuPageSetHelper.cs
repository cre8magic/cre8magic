using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Client.Pages.Internal;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Menus;

internal class MagicMenuPageSetHelper(MagicPageFactory pageFactory, MagicMenuTree tree) : MagicPageSetHelperBase(pageFactory)
{
    public void Set(MagicMenuSettings settings) => _settings = settings;

    /// <summary>
    /// Settings - on first access takes the one given, or creates a default.
    /// </summary>
    public MagicMenuSettings SettingsTyped => _settings ??= MagicMenuSettings.Defaults.Fallback;
    private MagicMenuSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IPageDesigner FallbackDesigner() => new MagicMenuDesigner(this, SettingsTyped);


    /// <summary>
    /// Retrieve the children the first time it's needed.
    /// </summary>
    /// <returns></returns>
    public List<MagicMenuPage> GetChildren(IMagicPage page)
    {
        var l = Log.Fn<List<MagicMenuPage>>($"{nameof(page.MenuLevel)}: {page.MenuLevel}");
        var levelsRemaining = tree.MaxDepth - (page.MenuLevel - 1 /* Level is 1 based, so -1 */);
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var children = pageFactory.ChildrenOf(page.Id)
            .Select(child => new MagicMenuPage(pageFactory, this, child, page.MenuLevel + 1))
            .ToList();
        return l.Return(children, $"{children.Count}");
    }

}