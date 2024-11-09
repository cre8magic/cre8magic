using Oqtane.Models;

namespace ToSic.Cre8magic.Pages;

public interface IMagicPage
{
    /// <summary>
    /// Level in the menu, starting from 1.
    /// As different menus may start at other depths, this is not always the same as the Oqtane level.
    /// </summary>
    int MenuLevel { get; }

    /// <summary>
    /// True if this page is the current page which the user is viewing.
    /// </summary>
    bool IsCurrent { get; }

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
    List<IMagicPage> Breadcrumb { get; }

    /// <summary>
    /// Determine if the menu page is in the breadcrumb.
    /// </summary>
    bool InBreadcrumb { get; }

    /// <summary>
    /// Original Oqtane page wrapped in MagicPage.
    /// </summary>
    Page OriginalPage { get; }

    /// <summary>
    /// Id of the Page
    /// </summary>
    int PageId { get; }

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
}