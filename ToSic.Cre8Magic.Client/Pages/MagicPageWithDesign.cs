using System.Collections;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Pages;

internal class MagicPageWithDesign : MagicPage, IMagicPageWithDesignWip, IMagicPageList, IEnumerable<IMagicPageWithDesignWip>
{
    /// <param name="pageFactory"></param>
    /// <param name="factory"></param>
    /// <param name="page">The original page.</param>
    internal MagicPageWithDesign(MagicPageFactory pageFactory, MagicPagesFactoryBase factory, IMagicPage? page = default, int? menuLevel = default)
        : base(page?.OqtanePage ?? pageFactory.PageState.Page, pageFactory)
    {
        Factory = factory;
        if (menuLevel.HasValue) MenuLevel = menuLevel.Value;
    }

    private MagicPagesFactoryBase Factory { get; }

    MagicPagesFactoryBase IMagicPageList.Factory => Factory;


    /// <summary>
    /// The ID of the menu item
    /// </summary>
    public string MenuId => Factory.Settings.MenuId;

    #region Children

    /// <summary>
    /// Get children of the current menu page.
    /// </summary>
    public IEnumerable<IMagicPageWithDesignWip> Children => _children ??= Factory.GetChildren(this);
    private IList<IMagicPageWithDesignWip>? _children;

    /// <summary>
    /// Determines if there are sub-pages. True if this page has sub-pages.
    /// </summary>
    public new bool HasChildren => _hasChildren ??= Children.Any();
    private bool? _hasChildren;


    #endregion

    #region Design

    private ITokenReplace TokenReplace => _nodeReplace ??= Factory.PageTokenEngine(this);
    private ITokenReplace? _nodeReplace;

    /// <inheritdoc cref="IMagicPageList.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(Factory.Design.Classes(tag, this)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageList.Value" />
    public string? Value(string key) => TokenReplace.Parse(Factory.Design.Value(key, this)).EmptyAsNull();

    public IMagicPageSetSettings Settings => Factory.Settings;

    #endregion


    public IEnumerator<IMagicPageWithDesignWip> GetEnumerator() => Children.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}