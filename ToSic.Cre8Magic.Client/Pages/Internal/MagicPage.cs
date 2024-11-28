using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Oqtane.Models;
using ToSic.Cre8magic.Pages.Internal.PageDesign;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Wrapper for the Oqtane Page.
/// </summary>
internal class MagicPage(Page oqtanePage, MagicPageFactory pageFactory, IMagicPageChildrenFactory childrenFactory)
    : MagicPageBase(oqtanePage),
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
    public bool IsActive => OqtanePage.PageId == pageFactory.PageState.Page.PageId;

    /// <inheritdoc />
    public bool IsHome => OqtanePage.Path == "";

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
    //private IList<IMagicPage>? _children;

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public override bool HasChildren => _hasChildren ??= Children.Any();
    private bool? _hasChildren;


    #endregion

    [field: AllowNull, MaybeNull]
    private IPageDesignHelperWip DesignHelper => field ??= childrenFactory.PageDesignHelper(this);

    /// <inheritdoc />
    public virtual string? Classes(string tag) => DesignHelper.Classes(tag);

    /// <inheritdoc />
    public virtual string? Value(string key) => DesignHelper.Value(key);

}