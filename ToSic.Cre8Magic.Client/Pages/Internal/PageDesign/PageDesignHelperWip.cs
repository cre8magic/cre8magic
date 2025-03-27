using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.Tokens;


namespace ToSic.Cre8magic.Pages.Internal.PageDesign;

internal class PageDesignHelperWip(MagicPagesFactoryBase pagesFactory, IMagicPage page, IMagicPageTailor pageTailor)
    : IMagicTailor
{
    [field: AllowNull, MaybeNull]
    private ITokenReplace TokenReplace => field ??= pagesFactory.TokenEngine.CloneWith(page);

    /// <inheritdoc cref="IMagicTailor.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(pageTailor.Classes(tag, page)).EmptyAsNull();

    /// <inheritdoc cref="IMagicTailor.Value" />
    public string? Value(string key) => TokenReplace.Parse(pageTailor.Value(key, page)).EmptyAsNull();

    /// <inheritdoc cref="IMagicTailor.Id" />
    public string? Id(string target) => TokenReplace.Parse(pageTailor.Value(target, page)).EmptyAsNull();
}