using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Client.Pages.Internal;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Menus;

/// <summary>
/// Represents a menu page in the MagicMenu system.
/// </summary>
/// <remarks>
/// Can't provide PageState from DI because that breaks Oqtane.
/// </remarks>
public class MagicMenuPage : MagicPageWithDesign, IMagicPageListOld
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagicMenuPage"/> class.
    /// </summary>
    /// <param name="pageFactory"></param>
    /// <param name="setHelper"></param>
    /// <param name="page">The original page.</param>
    /// <param name="level">The menu level.</param>
    internal MagicMenuPage(MagicPageFactory pageFactory, MagicPageSetHelperBase setHelper, IMagicPage page, int level)
        : base(pageFactory, setHelper, page)
    {
        MenuLevel = level;
    }

    /// <summary>
    /// Menu Level relative to the start of the menu (always starts with 1)
    /// </summary>
    /// <remarks>
    /// This is _not_ the same as Oqtane Page.Level
    /// </remarks>
    public override int MenuLevel { get; }

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public new bool HasChildren => _hasChildren ??= Children.Any();
    private bool? _hasChildren;

    /// <summary>
    /// The ID of the menu item
    /// </summary>
    public string MenuId => SetHelper.Settings.MenuId;

    /// <summary>
    /// Get children of the current menu page.
    /// </summary>
    public IList<MagicMenuPage> Children => _children ??= ((MagicMenuPageSetHelper)SetHelper).GetChildren(this);
    private IList<MagicMenuPage>? _children;
}