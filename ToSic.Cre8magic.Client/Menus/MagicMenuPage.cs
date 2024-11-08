using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Client.Models;
using ToSic.Cre8magic.Client.Pages;
using Log = ToSic.Cre8magic.Client.Logging.Log;

namespace ToSic.Cre8magic.Client.Menus;

/// <summary>
/// Represents a menu page in the MagicMenu system.
/// </summary>
/// <remarks>
/// Can't provide PageState from DI because that breaks Oqtane.
/// </remarks>
public class MagicMenuPage : MagicPageWithDesign
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagicMenuPage"/> class.
    /// </summary>
    /// <param name="pageFactory"></param>
    /// <param name="setHelper"></param>
    /// <param name="page">The original page.</param>
    /// <param name="level">The menu level.</param>
    /// <param name="tree">The magic menu tree.</param>
    /// <param name="debugPrefix">The debug prefix.</param>
    internal MagicMenuPage(MagicPageFactory pageFactory, MagicPageSetHelperBase setHelper, MagicPage page, int level, MagicMenuTree tree = null, string debugPrefix = null) : base(pageFactory, setHelper, page)
    {
        //SetHelper = setHelper;
        Level = level; // menu level

        Log = setHelper.LogRoot.GetLog(debugPrefix);

        if (tree == null) return;
        Tree = tree;
        var _ = PageInfo;   // Access page info early on to make logging nicer
    }

    public MagicMenuSettings Settings => ((MagicMenuPageSetHelper)SetHelper).Settings;

    /// <summary>
    /// Menu Level relative to the start of the menu (always starts with 1)
    /// </summary>
    /// <remarks>
    /// This is not the same as Oqtane Page.Level (which exists in base class).
    /// </remarks>
    public new int Level { get; init; }

    /// <summary>
    /// Root navigator object which has some data/logs for all navigators which spawned from it. 
    /// </summary>
    internal virtual MagicMenuTree Tree { get; }

    internal Log Log { get; set; }

    /// <summary>
    /// Special central place to get, cache and log the special properties only once
    /// </summary>
    internal MagicPageInfo PageInfo
    {
        get
        {
            if (_pI != null) return _pI;
            var l = Log.Fn<MagicPageInfo>($"Page: {PageId}");
            _pI = new()
            {
                HasChildren = Children.Any(),
                IsActive = PageId == Tree.PageId,
                InBreadcrumb = Tree.Breadcrumb.Contains(this),
            };
            return l.Return(_pI, $"Name: '{Name}': {_pI.Log}");
        }
    }

    private MagicPageInfo? _pI;

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public new bool HasChildren => PageInfo.HasChildren;

    /// <summary>
    /// Determine if the menu page is in the breadcrumb.
    /// </summary>
    public bool InBreadcrumb => PageInfo.InBreadcrumb;

    /// <summary>
    /// The ID of the menu item
    /// </summary>
    public string MenuId => _menuId ??= Settings?.MenuId ?? "error-menu-id";
    private string? _menuId;

    /// <summary>
    /// Get children of the current menu page.
    /// </summary>
    public IList<MagicMenuPage> Children => _children ??= GetChildren();
    private IList<MagicMenuPage>? _children;

    /// <summary>
    /// Retrieve the children the first time it's needed.
    /// </summary>
    /// <returns></returns>
    [return: NotNull]
    protected List<MagicMenuPage> GetChildren()
    {
        var l = Log.Fn<List<MagicMenuPage>>($"{nameof(Level)}: {Level}");
        var levelsRemaining = Tree.Depth - (Level - 1 /* Level is 1 based, so -1 */);
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var children = GetChildPages()
            .Select(page => new MagicMenuPage(PageFactory, SetHelper, page, Level + 1, Tree, $"{Log.Prefix}>{PageId}"))
            .ToList();
        return l.Return(children, $"{children.Count}");
    }

    private const string ErrPageNotFound = "Error: Page not found";

    protected virtual List<MagicPage> GetChildPages() => PageFactory.ChildrenOf(Tree.MenuPages, PageId);

}