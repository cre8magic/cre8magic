using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Utils;
using static System.StringComparison;


namespace ToSic.Cre8magic.Tokens;

internal class PageTokens(IMagicPage page, string? layoutVariant = null, string? menuId = null) : ITokenReplace
{
    /// <summary>
    /// Name of this Token Source
    /// </summary>
    public const string NameIdConstant = nameof(PageTokens);

    public IMagicPage Page { get; } = page;
    public string NameId => NameIdConstant;

    public PageTokens Clone(IMagicPage page, string? newMenuId = null) => new(page, layoutVariant, newMenuId ?? menuId);

    public string? Parse(string? classes)
    {
        // If there are no classes, exit
        if (!classes.HasValue())
            return classes;

        var result = classes
            .Replace(MagicTokens.PageId, $"{Page.Id}", InvariantCultureIgnoreCase);

        // If there are no placeholders left, exit
        if (!result.Contains(MagicTokens.PlaceholderMarker))
            return result;

        result = result
            .Replace(MagicTokens.PageParentId, Page.ParentId != null ? $"{Page.ParentId}" : MagicTokens.None)
            .Replace(MagicTokens.SiteId, $"{Page.RawPage.SiteId}", InvariantCultureIgnoreCase)
            .Replace(MagicTokens.LayoutVariation, layoutVariant ?? MagicTokens.None)
            .Replace(MagicTokens.MenuLevel, $"{Page.MenuLevel}")
            .Replace(MagicTokens.MenuId, menuId ?? MagicTokens.None);

        // Checking the breadcrumb is a bit more expensive, so be sure we need it
        if (result.Contains(MagicTokens.PageRootId))
            result = result
                .Replace(MagicTokens.PageRootId, CurrentPageRootId != null ? $"{CurrentPageRootId}" : MagicTokens.None);

        return result;
    }

    private int? CurrentPageRootId => _currentPageRootId.Get(() => Page.Breadcrumb.FirstOrDefault()?.Id);

    //private int? CurrentPageRootId
    //{
    //    get
    //    {
    //        if (_pageRootAlreadyTried) return _pageRootId;
    //        _pageRootAlreadyTried = true;
    //        _pageRootId = Page.Breadcrumb.FirstOrDefault()?.Id;
    //        return _pageRootId;
    //    }
    //}

    //private int? _pageRootId;
    //private bool _pageRootAlreadyTried;

    private readonly GetOnce<int?> _currentPageRootId = new();
}