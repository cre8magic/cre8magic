using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Breadcrumbs;

public class MagicBreadcrumbItem : MagicPage
{
    /// <param name="pageFactory"></param>
    /// <param name="page">The original page.</param>
    public MagicBreadcrumbItem(MagicPageFactory pageFactory, MagicPage? page = null, MagicBreadcrumb? breadcrumb = null) : base(page?.OriginalPage ?? pageFactory.PageState.Page, pageFactory)
    {
        Breadcrumb = breadcrumb;

        if (page != null && breadcrumb != null)
            IsActive = (page.PageId == breadcrumb.PageId);
        else
            IsActive = true;
    }

    /// <summary>
    /// Root navigator object which has some data/logs for all navigators which spawned from it. 
    /// </summary>
    internal virtual MagicBreadcrumb Breadcrumb { get; }

    /// <summary>
    /// Determine if the breadcrumb item is current page.
    /// </summary>
    public bool IsActive { get; }

    private ITokenReplace NodeReplace => _nodeReplace ??= Breadcrumb.PageTokenEngine(this);
    private ITokenReplace? _nodeReplace;

    /// <summary>
    /// Get css class for tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public string? Classes(string tag) => NodeReplace.Parse(Breadcrumb.Design.Classes(tag, this)).EmptyAsNull();

    /// <summary>
    /// Get attribute value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string? Value(string key) => NodeReplace.Parse(Breadcrumb.Design.Value(key, this)).EmptyAsNull();

}