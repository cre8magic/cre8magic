using Oqtane.Models;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
/// <param name="originalPage"></param>
public class MagicPage(Page originalPage, MagicPageFactory pageFactory): MagicPageBase(originalPage), IMagicPage
{
    protected MagicPageFactory PageFactory => pageFactory;

    /// <inheritdoc />
    public virtual int MenuLevel => Level + 1;

    /// <inheritdoc />
    public bool IsCurrent => OriginalPage.PageId == pageFactory.PageState.Page.PageId;

    /// <inheritdoc />
    public string Link => _link ??= pageFactory.PageProperties.GetUrl(this);
    private string? _link;

    /// <inheritdoc />
    public string? Target => _target ??= pageFactory.PageProperties.GetTarget(this);
    private string? _target;

    /// <inheritdoc />
    public List<IMagicPage> Breadcrumb => _breadcrumb ??= pageFactory.Breadcrumbs(this).ToList();
    private List<IMagicPage>? _breadcrumb;


    /// <inheritdoc />
    public bool InBreadcrumb => _inBreadcrumb ??= pageFactory.Current.Breadcrumb.Contains(this);
    private bool? _inBreadcrumb;


    public override string ToString() => $"{Name} ({Id})";

    #region Relationships

    
    public IMagicPage? Parent => _parent ??= ParentId == null ? null : pageFactory.GetOrNull(ParentId);
    private IMagicPage? _parent;

    #endregion
}