using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Pages.Internal.PageDesign;

internal class PageDesignHelperWip(MagicPagesFactoryBase pagesFactory, IMagicPage page, IMagicPageDesigner pageDesigner) : IPageDesignHelperWip
{
    [field: AllowNull, MaybeNull]
    private ITokenReplace TokenReplace => field ??= pagesFactory.TokenEngine.CloneWith(page);

    ///// <inheritdoc cref="IMagicPageList.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(pageDesigner.Classes(tag, page)).EmptyAsNull();

    ///// <inheritdoc cref="IMagicPageList.Value" />
    public string? Value(string key) => TokenReplace.Parse(pageDesigner.Value(key, page)).EmptyAsNull();

}