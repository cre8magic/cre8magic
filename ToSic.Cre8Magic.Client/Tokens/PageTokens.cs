using ToSic.Cre8magic.Client.Pages.Internal;
using ToSic.Cre8magic.Pages;
using static System.StringComparison;
using static ToSic.Cre8magic.Client.MagicTokens;

namespace ToSic.Cre8magic.Client.Tokens;

internal class PageTokens(
    MagicPageFactory pageFactory,
    IMagicPage? page = null,
    string? bodyClasses = null,
    string? id = null)
    : ITokenReplace
{
    public const string NameIdConstant = nameof(PageTokens);
    public IMagicPage? Page { get; } = page;
    public string NameId => NameIdConstant;

    public PageTokens Clone(IMagicPage page, string? menuId = null) => new(pageFactory, page, bodyClasses, menuId ?? id);

    public string Parse(string classes)
    {
        if (!classes.HasValue()) return classes;
        var page = Page ?? pageFactory.Current;
        var result = classes
            .Replace(PageId, $"{page.Id}", InvariantCultureIgnoreCase);

        // If there are no placeholders left, exit
        if (!result.Contains(PlaceholderMarker)) return result;

        result = result
            .Replace(PageParentId, page.ParentId != null ? $"{page.ParentId}" : None)
            .Replace(SiteId, $"{page.OqtanePage.SiteId}", InvariantCultureIgnoreCase)
            .Replace(LayoutVariation, bodyClasses ?? None)
            .Replace(MenuLevel, $"{page.MenuLevel}")
            .Replace(MenuId, id ?? None);

        // Checking the breadcrumb is a bit more expensive, so be sure we need it
        if (result.Contains(PageRootId))
            result = result
                .Replace(PageRootId, CurrentPageRootId != null ? $"{CurrentPageRootId}" : None);

        return result;
    }

    private int? CurrentPageRootId
    {
        get
        {
            if (_pageRootAlreadyTried) return _pageRootId;
            _pageRootAlreadyTried = true;
            _pageRootId = pageFactory.Breadcrumb.Get().FirstOrDefault()?.Id;
            return _pageRootId;
        }
    }

    private int? _pageRootId;
    private bool _pageRootAlreadyTried;
}