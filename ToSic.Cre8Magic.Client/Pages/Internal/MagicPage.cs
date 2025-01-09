using System.Diagnostics.CodeAnalysis;
using Oqtane.Models;
using ToSic.Cre8magic.Menus.Internal.PagePicker;
using ToSic.Cre8magic.Pages.Internal.PageDesign;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
internal record MagicPage : MagicPageBase,
        IMagicPage
{
    public MagicPage(Page RawPage, MagicPageFactory pageFactory, IMagicPageChildrenFactory childrenFactory) : base(RawPage)
    {
        _pageFactory = pageFactory;
        _childrenFactory = childrenFactory;
    }
    private readonly MagicPageFactory _pageFactory;
    private readonly IMagicPageChildrenFactory _childrenFactory;

    /// <inheritdoc />
    public int MenuLevel
    {
        get => _menuLevel ??= Level + 1;
        init => _menuLevel = value;
    }
    private int? _menuLevel;

    // TODO: WIP
    internal bool IsVirtualRoot { get; init; }

    /// <inheritdoc />
    public bool IsActive
        => RawPage.PageId == _pageFactory.PageState.Page.PageId;

    /// <inheritdoc />
    public bool IsHome
        => RawPage.Path == "";

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public string Link
        => field ??= _pageFactory.PageProperties.GetLink(this);

    /// <inheritdoc />
    public string? Target
        => field ??= _pageFactory.PageProperties.GetTarget(this);

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public IEnumerable<IMagicPage> Breadcrumb
        => field ??= _pageFactory.Breadcrumb.Get(new() { Active = this }).Pages;


    /// <inheritdoc />
    public bool IsInBreadcrumb
        => _isInBreadcrumb ??= _pageFactory.Current.Breadcrumb.Contains(this);
    private bool? _isInBreadcrumb;


    public override string ToString()
        => $"{Name} ({Id})";

    #region Relationships

    
    public IMagicPage? Parent
    {
        get
        {
            if (_parentAlreadyTried) return field;
            _parentAlreadyTried = true;
            return field ??= ParentId == null ? null : _pageFactory.GetOrNull(ParentId);
        }
    }

    private bool _parentAlreadyTried;

    /// <summary>
    /// Get children of the current menu page.
    /// </summary>
    [field: AllowNull, MaybeNull]
    public IEnumerable<IMagicPage> Children => field ??= _childrenFactory.ChildrenOf(this);

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public override bool HasChildren => _hasChildren ??= Children.Any();
    private bool? _hasChildren;


    #endregion

    [field: AllowNull, MaybeNull]
    private IPageDesignHelperWip DesignHelper
        => field ??= _childrenFactory.PageDesignHelper(this);

    /// <inheritdoc />
    public virtual string? Classes(string tagOrKey)
        => DesignHelper.Classes(tagOrKey);

    /// <inheritdoc />
    public virtual string? Value(string tagOrKey)
        => DesignHelper.Value(tagOrKey);

    internal PagesPickRule? NodeRule { get; init; }

}