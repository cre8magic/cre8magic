namespace ToSic.Cre8magic.Pages;

/// <summary>
/// Specs for creating a breadcrumb.
///
/// WIP because it should probably be later merged with Breadcrumb Settings into a simple, single object.
/// </summary>
public record MagicMenuGetSpecsWip
{
    /// <summary>
    /// Magic Settings for debugging, in case these specs were generated from there.
    /// </summary>
    public MagicSettings? MagicSettings { get; init; }

    /// <summary>
    /// How many level deep the navigation should show.
    /// The number is ??? relative,
    /// so if the navigation starts an level 2 then levelDepth 2 means to show levels 2 & 3 ??? verify
    /// </summary>
    public int? Depth { get; set; }

    /// <summary>
    /// Levels to skip from the initial stating point.
    /// - 0 means don't skip any, so if we're starting at the root, show that level
    /// - 1 means skip the first level, so if we're starting at the root, show the children
    /// See inspiration context from DDRMenu https://www.dnnsoftware.com/wiki/ddrmenu-reference-guide
    /// in DDR it was called 'skip' but it didn't make sense IMHO
    /// </summary>
    public bool? Children { get; set; }

    /// <summary>
    /// The level this menu should start from.
    /// - `0` is the top level (default)
    /// - `1` is the top level containing home and other pages
    /// - `-1` is one level up from the current node
    /// - `-2` is two levels up from the current node
    /// </summary>
    public int? Level { get; set; }


    public MagicMenuSettings? Settings { get; init; }

    public IMagicPageDesigner? Designer { get; init; }

    /// <summary>
    /// WIP moving here
    /// </summary>
    public string? Start { get; set; }

    ///// <summary>
    ///// Maximum depth of the breadcrumb, defaults to 10.
    ///// This is to ensure that we don't run into infinite loops.
    ///// </summary>
    //public int MaxDepth { get; init; } = 10;

    ///// <summary>
    ///// If the order of the Breadcrumb should be reversed.
    ///// </summary>
    //public bool Reverse { get; init; } = false;

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    public IEnumerable<IMagicPage>? Pages { get; init; } = null;

    //// TODO: NAMING
    //public IMagicPage? Current { get; init; }

    public List<string> DebugMessages { get; init; } = [];

}