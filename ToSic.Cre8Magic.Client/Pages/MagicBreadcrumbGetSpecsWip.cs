using ToSic.Cre8magic.Client.Breadcrumb.Settings;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages;

/// <summary>
/// Specs for creating a breadcrumb.
///
/// WIP because it should probably be later merged with Breadcrumb Settings into a simple, single object.
/// </summary>
public record MagicBreadcrumbGetSpecsWip
{
    /// <summary>
    /// If the current page should be included in the breadcrumb.
    ///
    /// Set to false for scenarios where you don't want to show the final page,
    /// or will use custom code to visualize differently.
    /// </summary>
    public bool WithCurrent { get; init; } = true;

    /// <summary>
    /// If the home page should be included in the breadcrumb.
    /// This is special because the home page is usually not really "above" the others, but typically side-by side to other pages on the top level menu.
    ///
    /// Set to false, if you only want to show the breadcrumb starting at the level below home.
    /// </summary>
    public bool WithHome { get; init; } = true;

    public MagicBreadcrumbSettings? Settings { get; init; }

    public IPageDesigner? Designer { get; init; }

    /// <summary>
    /// Maximum depth of the breadcrumb, defaults to 10.
    /// This is to ensure that we don't run into infinite loops.
    /// </summary>
    public int MaxDepth { get; init; } = 10;

    /// <summary>
    /// If the order of the Breadcrumb should be reversed.
    /// </summary>
    public bool Reverse { get; init; } = false;

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    public IEnumerable<IMagicPage>? Pages { get; init; } = null;

    // TODO: NAMING
    public IMagicPage? Current { get; init; }
}