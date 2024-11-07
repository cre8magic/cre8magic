using Oqtane.Shared;
using Oqtane.UI;

namespace ToSic.Cre8magic.Client.Models;

/// <summary>
/// This class provides functionality for the menu control.
/// It is based on the core 'oqtane.framework\Oqtane.Client\Themes\Controls\Theme\MenuBase.cs'
/// but it favors composition over inheritance.
/// </summary>
/// <remarks>
/// Can't provide PageState from DI because that breaks Oqtane.
/// </remarks>
internal class MagicPageProperties(MagicPageFactory pageFactory)
{
    /// <summary>
    /// Link to page.
    /// </summary>
    public string GetUrl(MagicPage page) 
        => page.IsClickable
            ? string.IsNullOrEmpty(page.Url) ? NavigateUrl(page.Path) : page.Url
            : "javascript:void(0)";

    /// <summary>
    /// Target for link to page.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public string? GetTarget(MagicPage page)
        => page.Url?.StartsWith("http") == true ? "_blank" : null;



    private string NavigateUrl(string path, string? parameters = null)
        => Utilities.NavigateUrl(pageFactory.PageState.Alias.Path, path, parameters);
}