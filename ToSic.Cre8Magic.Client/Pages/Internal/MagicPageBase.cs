using Oqtane.Models;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
internal record MagicPageBase(Page RawPage)
{
    /// <summary>
    /// Original Oqtane page wrapped in MagicPage.
    /// </summary>
    public Page RawPage { get; } = RawPage;

    /// <summary>
    /// ID of the Page
    /// </summary>
    public int Id => RawPage.PageId;

    /// <summary>
    /// Reference to the parent <see cref="Page"/> if it has one.
    /// </summary>
    public int? ParentId => RawPage.ParentId;

    /// <summary>
    /// Path of the page.
    /// </summary>
    public string Path => RawPage.Path;

    /// <summary>
    /// Page Name.
    /// </summary>
    public string Name => RawPage.Name;

    /// <summary>
    /// Full URL to this page.
    /// </summary>
    public string Url => RawPage.Url;

    /// <summary>
    /// Link in site navigation is enabled or disabled.
    /// </summary>
    public bool IsClickable => RawPage.IsClickable;

    /// <summary>
    /// Current page level from the top of the Menu.
    /// As in Oqtane, it's 0 based.
    /// </summary>
    public int Level => RawPage.Level;

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public virtual bool HasChildren => RawPage.HasChildren;
    
}