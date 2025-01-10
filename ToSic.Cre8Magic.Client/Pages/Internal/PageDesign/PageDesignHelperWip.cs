using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Internal;

namespace ToSic.Cre8magic.Pages.Internal.PageDesign;

internal class PageDesignHelperWip(MagicPagesFactoryBase pagesFactory, IMagicPage page, IMagicPageTailor pageTailor) : IPageDesignHelperWip
{
    [field: AllowNull, MaybeNull]
    private ITokenReplace TokenReplace => field ??= pagesFactory.TokenEngine.CloneWith(page);

    ///// <inheritdoc cref="IMagicPageList.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(pageTailor.Classes(tag, page)).EmptyAsNull();

    ///// <inheritdoc cref="IMagicPageList.Value" />
    public string? Value(string key) => TokenReplace.Parse(pageTailor.Value(key, page)).EmptyAsNull();

}