using System.Collections;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal;

internal class MagicPageList(MagicPageFactory pageFactory, MagicPageSetHelperBase helper, IEnumerable<IMagicPageWithDesignWip> items): IMagicPageList
{
    public int MenuLevel => 1;

    internal MagicPageSetHelperBase SetHelper => helper;

    // ReSharper disable once NotDisposedResourceIsReturned
    public IEnumerator<IMagicPageWithDesignWip> GetEnumerator() => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    private IMagicPage VPageLevel1 => _vPageLevel1 ??= new MagicPage(new() { Level = 0 /* Level is 0, so MenuLevel will be 1 */ }, pageFactory);
    private IMagicPage? _vPageLevel1;

    private ITokenReplace TokenReplace => _nodeReplace ??= helper.PageTokenEngine(VPageLevel1);
    private ITokenReplace? _nodeReplace;

    /// <inheritdoc cref="IMagicPageList.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(helper.Design.Classes(tag, VPageLevel1)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageList.Value" />
    public string? Value(string key) => TokenReplace.Parse(helper.Design.Value(key, VPageLevel1)).EmptyAsNull();

    public IMagicPageSetSettings Settings => helper.Settings;
}