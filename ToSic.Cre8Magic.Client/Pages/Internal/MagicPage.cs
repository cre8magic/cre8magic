using System.Diagnostics.CodeAnalysis;
using Oqtane.Models;
using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages.Internal.PageDesign;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
internal record MagicPage(Page RawPage, MagicPageFactory pageFactory, IMagicPageChildrenFactory childrenFactory)
    : MagicPageBase(RawPage),
        IMagicPage
{
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
    public bool IsActive => RawPage.PageId == pageFactory.PageState.Page.PageId;

    /// <inheritdoc />
    public bool IsHome => RawPage.Path == "";

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public string Link => field ??= pageFactory.PageProperties.GetLink(this);

    /// <inheritdoc />
    public string? Target => field ??= pageFactory.PageProperties.GetTarget(this);

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public IEnumerable<IMagicPage> Breadcrumb => field ??= pageFactory.Breadcrumb.Get(new() { Active = this }).Pages;


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

    /// <summary>
    /// Get children of the current menu page.
    /// </summary>
    [field: AllowNull, MaybeNull]
    public IEnumerable<IMagicPage> Children => field ??= childrenFactory.ChildrenOf(this);

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public override bool HasChildren => _hasChildren ??= Children.Any();
    private bool? _hasChildren;


    #endregion

    [field: AllowNull, MaybeNull]
    private IPageDesignHelperWip DesignHelper => field ??= childrenFactory.PageDesignHelper(this);

    /// <inheritdoc />
    public virtual string? Classes(string tagOrKey) => DesignHelper.Classes(tagOrKey);

    /// <inheritdoc />
    public virtual string? Value(string tagOrKey) => DesignHelper.Value(tagOrKey);

    internal StartNodeRule? NodeRule { get; init; }
}