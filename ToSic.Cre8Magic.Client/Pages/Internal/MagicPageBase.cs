using Oqtane.Models;

namespace ToSic.Cre8magic.Client.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
internal class MagicPageBase(Page oqtanePage)
{
    /// <summary>
    /// Original Oqtane page wrapped in MagicPage.
    /// </summary>
    public Page OqtanePage { get; } = oqtanePage;

    /// <summary>
    /// ID of the Page
    /// </summary>
    public int Id => OqtanePage.PageId;

    /// <summary>
    /// Reference to the parent <see cref="Page"/> if it has one.
    /// </summary>
    public int? ParentId => OqtanePage.ParentId;

    /// <summary>
    /// Path of the page.
    /// </summary>
    public string Path => OqtanePage.Path;

    /// <summary>
    /// Page Name.
    /// </summary>
    public string Name => OqtanePage.Name + "-234";

    /// <summary>
    /// Full URL to this page.
    /// </summary>
    public string Url => OqtanePage.Url;

    /// <summary>
    /// Link in site navigation is enabled or disabled.
    /// </summary>
    public bool IsClickable => OqtanePage.IsClickable;

    /// <summary>
    /// Current page level from the top of the Menu.
    /// As in Oqtane, it's 0 based.
    /// </summary>
    public int Level => OqtanePage.Level;

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public bool HasChildren => OqtanePage.HasChildren;
    
}