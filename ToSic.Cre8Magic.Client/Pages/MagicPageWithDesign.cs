using ToSic.Cre8magic.Client.Pages.Internal;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages;

public class MagicPageWithDesign : MagicPage
{
    /// <param name="pageFactory"></param>
    /// <param name="setHelper"></param>
    /// <param name="page">The original page.</param>
    internal MagicPageWithDesign(MagicPageFactory pageFactory, MagicPageSetHelperBase setHelper, IMagicPage? page = null) : base(page?.OriginalPage ?? pageFactory.PageState.Page, pageFactory)
    {
        SetHelper = setHelper;
    }

    internal MagicPageSetHelperBase SetHelper { get; }

    private ITokenReplace TokenReplace => _nodeReplace ??= SetHelper.PageTokenEngine(this);
    private ITokenReplace? _nodeReplace;

    /// <inheritdoc cref="IMagicPageList.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(SetHelper.Design.Classes(tag, this)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageList.Value" />
    public string? Value(string key) => TokenReplace.Parse(SetHelper.Design.Value(key, this)).EmptyAsNull();

}