using Oqtane.Models;

namespace ToSic.Cre8magic.Client.Models;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
/// <param name="originalPage"></param>
public class MagicPage(Page originalPage, MagicPageFactory pageFactory): MagicPageBase(originalPage)
{
    protected MagicPageFactory PageFactory => pageFactory;

    /// <summary>
    /// Level in the menu, starting from 1.
    /// As different menus may start at other depths, this is not always the same as the Oqtane level.
    /// </summary>
    public virtual int MenuLevel => Level + 1;

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

    /// <summary>
    /// The current pages bread-crumb, going from the top-level to the current page.
    /// Note that the "Home" page is usually not a parent, so it's not included.
    /// </summary>
    public List<MagicPage> Breadcrumb => _breadcrumb ??= PageFactory.Breadcrumbs(this).ToList();
    private List<MagicPage>? _breadcrumb;


    /// <summary>
    /// Determine if the menu page is in the breadcrumb.
    /// </summary>
    public bool InBreadcrumb => _inBreadcrumb ??= PageFactory.Current.Breadcrumb.Contains(this);
    private bool? _inBreadcrumb;


}