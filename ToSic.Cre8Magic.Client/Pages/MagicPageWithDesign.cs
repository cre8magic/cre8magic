using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Pages;

public class MagicPageWithDesign : MagicPage
{
    /// <param name="pageFactory"></param>
    /// <param name="helper">The helper - or null in the first breadcrumb item</param>
    /// <param name="page">The original page.</param>
    internal MagicPageWithDesign(MagicPageFactory pageFactory, MagicPageHelperBase helper, MagicPage? page = null) : base(page?.OriginalPage ?? pageFactory.PageState.Page, pageFactory)
    {
        Helper = helper;
    }

    internal MagicPageHelperBase Helper { get; }

    private ITokenReplace TokenReplace => _nodeReplace ??= Helper.PageTokenEngine(this);
    private ITokenReplace? _nodeReplace;

    /// <summary>
    /// Get css class for tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public string? Classes(string tag) => TokenReplace.Parse(Helper.Design.Classes(tag, this)).EmptyAsNull();

    /// <summary>
    /// Get attribute value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string? Value(string key) => TokenReplace.Parse(Helper.Design.Value(key, this)).EmptyAsNull();

}