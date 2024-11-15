using System.Collections;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Pages.Internal;

internal class MagicPageList(IContextWip context, MagicPageFactory pageFactory, MagicPagesFactoryBase factory, IEnumerable<IMagicPageWithDesignWip> items): IMagicPageList
{
    public int MenuLevel => 1;

    //internal MagicPagesFactoryBase Factory => factory;

    //MagicPagesFactoryBase IMagicPageListInternal.Factory2 => factory;
    MagicPagesFactoryBase IMagicPageList.Factory => factory;

    // ReSharper disable once NotDisposedResourceIsReturned
    public IEnumerator<IMagicPageWithDesignWip> GetEnumerator() => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    private IMagicPage VPageLevel1 => _vPageLevel1 ??= new MagicPage(new() { Level = 0 /* Level is 0, so MenuLevel will be 1 */ }, pageFactory);
    private IMagicPage? _vPageLevel1;

    private ITokenReplace TokenReplace => _nodeReplace ??= factory.PageTokenEngine(VPageLevel1);
    private ITokenReplace? _nodeReplace;

    /// <inheritdoc cref="IMagicPageList.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(factory.Design.Classes(tag, VPageLevel1)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageList.Value" />
    public string? Value(string key) => TokenReplace.Parse(factory.Design.Value(key, VPageLevel1)).EmptyAsNull();

    public IMagicPageSetSettings Settings => factory.Settings;

    IContextWip IMagicPageList.Context => context;
}