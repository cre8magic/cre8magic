using Oqtane.Models;

namespace ToSic.Cre8magic.Pages;

/// <summary>
/// Magic Pages are smart wrappers around Oqtane Pages.
/// They offer many benefits such as:
///
/// 1. Read-only properties (no accidental writing properties with unexpected side effects)
/// 1. Improved naming, e.g. `.Id` instead of `.PageId`
/// 1. Calculated properties such as `Target` (which becomes `"_blank"` for external links or `null` for normal links)
/// 1. Corrected values - e.g. the `Link` property will be `javascript:void(0)` if the page is not clickable
/// 1. Navigation properties such as `Parent` and `Breadcrumb`
/// </summary>
public interface IMagicPage
{
    /// <summary>
    /// Level in the menu, starting from 1.
    /// As different menus may start at other depths, this is not always the same as the Oqtane level.
    /// </summary>
    int MenuLevel { get; }

    /// <summary>
    /// True if this page is the active / current page which the user is viewing.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// True if this is the home page.
    /// </summary>
    bool IsHome { get; }

    /// <summary>
    /// Link to this page.
    /// </summary>
    string Link { get; }

    /// <summary>
    /// Target for link to this page.
    /// </summary>
    string? Target { get; }

    /// <summary>
    /// The current pages bread-crumb, going from the top-level to the current page.
    /// Note that the "Home" page is usually not a parent, so it's not included.
    /// </summary>
    IEnumerable<IMagicPage> Breadcrumb { get; }

    /// <summary>
    /// Determine if the menu page is in the breadcrumb.
    /// </summary>
    bool IsInBreadcrumb { get; }

    /// <summary>
    /// Original Oqtane page wrapped in MagicPage.
    /// Can be used to access additional properties of the Oqtane page.
    /// </summary>
    Page OqtanePage { get; }

    /// <summary>
    /// ID of this Page
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Reference to the parent <see cref="Page"/> if it has one.
    /// </summary>
    int? ParentId { get; }

    /// <summary>
    /// Path of the page.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Page Name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Full URL to this page.
    /// </summary>
    string Url { get; }

    /// <summary>
    /// Link in site navigation is enabled or disabled.
    /// </summary>
    bool IsClickable { get; }

    /// <summary>
    /// Current page level from the top of the Menu.
    /// As in Oqtane, it's 0 based.
    /// </summary>
    int Level { get; }

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    bool HasChildren { get; }

    #region Relationships

    /// <summary>
    /// The parent page of this page.
    /// </summary>
    /// <returns>
    /// The parent page, or null if the page is on the top level and doesn't have a parent page.
    /// </returns>
    IMagicPage? Parent { get; }

    #endregion
}