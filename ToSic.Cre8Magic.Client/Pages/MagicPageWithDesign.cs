using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Client.Pages.Internal;

// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

public class MagicPageWithDesign : MagicPage, IMagicPageWithDesignWip, IMagicPageListOld
{
    /// <param name="pageFactory"></param>
    /// <param name="setHelper"></param>
    /// <param name="page">The original page.</param>
    internal MagicPageWithDesign(MagicPageFactory pageFactory, MagicPageSetHelperBase setHelper, IMagicPage? page = default, int? menuLevel = default)
        : base(page?.OqtanePage ?? pageFactory.PageState.Page, pageFactory)
    {
        SetHelper = setHelper;
        if (menuLevel.HasValue) MenuLevel = menuLevel.Value;
    }

    internal MagicPageSetHelperBase SetHelper { get; }

    /// <summary>
    /// The ID of the menu item
    /// </summary>
    public string MenuId => SetHelper.Settings.MenuId;

    #region Children

    /// <summary>
    /// Get children of the current menu page.
    /// </summary>
    public IEnumerable<IMagicPageWithDesignWip> Children => _children ??= SetHelper.GetChildren(this);
    private IList<IMagicPageWithDesignWip>? _children;

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public new bool HasChildren => _hasChildren ??= Children.Any();
    private bool? _hasChildren;


    #endregion

    #region Design

    private ITokenReplace TokenReplace => _nodeReplace ??= SetHelper.PageTokenEngine(this);
    private ITokenReplace? _nodeReplace;

    /// <inheritdoc cref="IMagicPageListOld.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(SetHelper.Design.Classes(tag, this)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageListOld.Value" />
    public string? Value(string key) => TokenReplace.Parse(SetHelper.Design.Value(key, this)).EmptyAsNull();

    #endregion


}