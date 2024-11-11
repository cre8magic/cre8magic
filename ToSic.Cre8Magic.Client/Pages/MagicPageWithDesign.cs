using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Client.Pages.Internal;

// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

public class MagicPageWithDesign : MagicPage, IMagicPageWithDesignWip
{
    /// <param name="pageFactory"></param>
    /// <param name="setHelper"></param>
    /// <param name="page">The original page.</param>
    internal MagicPageWithDesign(MagicPageFactory pageFactory, MagicPageSetHelperBase setHelper, IMagicPage? page = null) : base(page?.OqtanePage ?? pageFactory.PageState.Page, pageFactory)
    {
        SetHelper = setHelper;
    }

    internal MagicPageSetHelperBase SetHelper { get; }

    private ITokenReplace TokenReplace => _nodeReplace ??= SetHelper.PageTokenEngine(this);
    private ITokenReplace? _nodeReplace;

    /// <inheritdoc cref="IMagicPageListOld.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(SetHelper.Design.Classes(tag, this)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageListOld.Value" />
    public string? Value(string key) => TokenReplace.Parse(SetHelper.Design.Value(key, this)).EmptyAsNull();

}