using Oqtane.Models;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
/// <param name="oqtanePagege"></param>
public class MagicPage(Page oqtanePage, MagicPageFactory pageFactory): MagicPageBase(oqtanePage), IMagicPage
{
    protected MagicPageFactory PageFactory => pageFactory;

    /// <inheritdoc />
    public virtual int MenuLevel => Level + 1;

    /// <inheritdoc />
    public bool IsCurrent => OqtanePage.PageId == pageFactory.PageState.Page.PageId;

    /// <inheritdoc />
    public string Link => _link ??= pageFactory.PageProperties.GetLink(this);
    private string? _link;

    /// <inheritdoc />
    public string? Target => _target ??= pageFactory.PageProperties.GetTarget(this);
    private string? _target;

    /// <inheritdoc />
    public IEnumerable<IMagicPage> Breadcrumb => _breadcrumb ??= pageFactory.Breadcrumb.Get(new MagicBreadcrumbGetSpecsWip { Current = this }).ToList();
    private IEnumerable<IMagicPage>? _breadcrumb;


    /// <inheritdoc />
    public bool IsInBreadcrumb => _isInBreadcrumb ??= pageFactory.Current.Breadcrumb.Contains(this);
    private bool? _isInBreadcrumb;


    public override string ToString() => $"{Name} ({Id})";

    #region Relationships

    
    public IMagicPage? Parent => _parent ??= ParentId == null ? null : pageFactory.GetOrNull(ParentId);
    private IMagicPage? _parent;

    #endregion
}