using Oqtane.Models;
using ToSic.Cre8magic.Breadcrumb.Settings;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
internal class MagicPage(Page oqtanePage, MagicPageFactory pageFactory): MagicPageBase(oqtanePage), IMagicPage
{
    protected MagicPageFactory PageFactory => pageFactory;

    /// <inheritdoc />
    public int MenuLevel
    {
        get => _menuLevel ??= Level + 1;
        init => _menuLevel = value;
    }
    private int? _menuLevel;

    /// <inheritdoc />
    public bool IsActive => OqtanePage.PageId == pageFactory.PageState.Page.PageId;

    /// <inheritdoc />
    public bool IsHome => OqtanePage.Path == "";

    /// <inheritdoc />
    public string Link => _link ??= pageFactory.PageProperties.GetLink(this);
    private string? _link;

    /// <inheritdoc />
    public string? Target => _target ??= pageFactory.PageProperties.GetTarget(this);
    private string? _target;

    /// <inheritdoc />
    public IEnumerable<IMagicPage> Breadcrumb => _breadcrumb ??= pageFactory.Breadcrumb.Get(new MagicBreadcrumbSettings { Active = this }).ToList();
    private IEnumerable<IMagicPage>? _breadcrumb;


    /// <inheritdoc />
    public bool IsInBreadcrumb => _isInBreadcrumb ??= pageFactory.Current.Breadcrumb.Contains(this);
    private bool? _isInBreadcrumb;


    public override string ToString() => $"{Name} ({Id})";

    #region Relationships

    
    public IMagicPage? Parent
    {
        get
        {
            if (_parentAlreadyTried) return _parent;
            _parentAlreadyTried = true;
            return _parent ??= ParentId == null ? null : pageFactory.GetOrNull(ParentId);
        }
    }

    private IMagicPage? _parent;
    private bool _parentAlreadyTried;

    #endregion
}