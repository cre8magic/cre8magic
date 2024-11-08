using Oqtane.Models;

namespace ToSic.Cre8magic.Client.Models;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
/// <param name="originalPage"></param>
public class MagicPage(Page originalPage, MagicPageFactory pageFactory)
{
    protected MagicPageFactory PageFactory => pageFactory;

    /// <summary>
    /// Original Oqtane page wrapped in MagicPage.
    /// </summary>
    public Page OriginalPage { get; } = originalPage;

    /// <summary>
    /// Id of the Page
    /// </summary>
    public int PageId => OriginalPage.PageId;

    /// <summary>
    /// Reference to the parent <see cref="Page"/> if it has one.
    /// </summary>
    public int? ParentId => OriginalPage.ParentId;

    /// <summary>
    /// Path of the page.
    /// </summary>
    public string Path => OriginalPage.Path;

    /// <summary>
    /// Page Name.
    /// </summary>
    public string Name => OriginalPage.Name + "-test";

    /// <summary>
    /// Full URL to this page.
    /// </summary>
    public string Url => OriginalPage.Url;

    /// <summary>
    /// Link in site navigation is enabled or disabled.
    /// </summary>
    public bool IsClickable => OriginalPage.IsClickable;

    /// <summary>
    /// Current page level from the top of the Menu
    /// </summary>
    public int Level => OriginalPage.Level;

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public bool HasChildren => OriginalPage.HasChildren;




    /// <summary>
    /// True if this page is the current page which the user is viewing.
    /// </summary>
    public bool IsCurrent => OriginalPage.PageId == pageFactory.PageState.Page.PageId;

    /// <summary>
    /// Link to this page.
    /// </summary>
    public string Link => _link ??= pageFactory.PageProperties.GetUrl(this);
    private string? _link;

    /// <summary>
    /// Target for link to this page.
    /// </summary>
    public string? Target => _target ??= pageFactory.PageProperties.GetTarget(this);
    private string? _target;

    public List<MagicPage> Breadcrumb => _breadcrumb ??= PageFactory.Breadcrumbs(this).ToList();
    private List<MagicPage>? _breadcrumb;



}