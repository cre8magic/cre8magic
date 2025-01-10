namespace ToSic.Cre8magic;

public class MagicTokens
{
    /// <summary>
    /// This will be used as value if a value is null/empty.
    /// For example, it would give a page-parent-none if there is no parent
    /// </summary>
    internal const string None = "none";
    internal const string PlaceholderMarker = "[";

    internal const string MenuId = "[Menu.Id]";

    /// <summary>
    /// Menu-Level placeholder, typically for CSS-classes applied to `li` or `a` elements.
    /// </summary>
    /// <example>
    /// The class for the `li` could be `level-[Menu.Level]`
    /// </example>
    public const string MenuLevel = "[Menu.Level]";

    /// <summary>
    /// Placeholder for the current Module ID.
    /// </summary>
    public const string ModuleId = "[Module.Id]";

    internal const string ModuleControlName = "[Module.ControlName]";
    internal const string ModuleNamespace = "[Module.Namespace]";

    /// <summary>
    /// Placeholder for the current page ID.
    /// </summary>
    /// <example>
    /// In this: Products (7) - Kitchenware (73) - Pans (739) - Frying Pan (7391)
    ///
    /// The ID of the `Pans` page is 739
    /// </example>
    public const string PageId = "[Page.Id]";

    /// <summary>
    /// Placeholder for the current pages parent-page ID.
    /// </summary>
    /// <remarks>
    /// In this: Products (7) - Kitchenware (73) - Pans (739) - Frying Pan (7391)
    ///
    /// The Parent ID of the `Pans` page is 73
    /// </remarks>
    public const string PageParentId = "[Page.ParentId]";

    /// <summary>
    /// Placeholder for the current pages root-page ID.
    /// </summary>
    /// <remarks>
    /// Placeholder for the current pages parent-page ID.
    /// In this: Products (7) - Kitchenware (73) - Pans (739) - Frying Pan (7391)
    ///
    /// The Root ID of the `Pans` page is 7
    /// </remarks>
    public const string PageRootId = "[Page.RootId]";

    /// <summary>
    /// Placeholder for the current Site ID.
    /// </summary>
    internal const string SiteId = "[Site.Id]";

    /// <summary>
    /// Url to the theme folder, like for linking to an image files.
    /// </summary>
    /// <example>
    /// Link to a logo: `[Theme.Url]/assets/logo.png`
    /// </example>
    public const string ThemeUrl = "[Theme.Url]";

    // TODO! naming
    internal const string LayoutVariation = "[Layout.Variation]";

    /// <summary>
    /// Special key to mark rules "ByLevel" which apply to all level which had not been defined
    /// </summary>
    public const int ByLevelOtherKey = -1;
}