using System.Collections;
using Oqtane.UI;
using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Client.Pages.Internal;
using ToSic.Cre8magic.Client.Pages.Internal.Menu;
using ToSic.Cre8magic.Pages;
using Log = ToSic.Cre8magic.Client.Logging.Log;

namespace ToSic.Cre8magic.Client.Menus;

public class MagicMenuTree : IMagicPageList
{
    public MagicMenuTree(PageState pageState, MagicMenuGetSpecsWip specs)
    {
        Specs = specs;
        RootBuilder = new MagicMenuRootBuilder(pageState, specs);
        PageFactory = RootBuilder.PageFactory;
        SetHelper = RootBuilder.SubSetHelper;
        Log = SetHelper.LogRoot.GetLog("Root");
        Log.A($"Start with PageState for Page:{PageFactory.Current.Id}; Level:1");
    }

    private MagicMenuRootBuilder RootBuilder { get; }

    internal MagicMenuGetSpecsWip Specs { get; }

    internal Log Log { get; }

    internal MagicMenuSubBuilder SetHelper { get; }
    private MagicPageFactory PageFactory { get; }

    public int MenuLevel => 1;

    public bool HasChildren => Children.Any();

    public IEnumerable<IMagicPageWithDesignWip> Children => _children ??= RootBuilder.GetChildren();
    private IList<IMagicPageWithDesignWip>? _children;
    

    private ITokenReplace TokenReplace => _nodeReplace ??= SetHelper.PageTokenEngine(VPageLevel1);
    private ITokenReplace? _nodeReplace;

    private IMagicPage VPageLevel1 => _vPageLevel1 ??= new MagicPage(new() { Level = 0 /* Level is 0, so MenuLevel will be 1 */ }, PageFactory);
    private IMagicPage? _vPageLevel1;

    /// <inheritdoc cref="IMagicPageList.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(SetHelper.Design.Classes(tag, VPageLevel1)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageList.Value" />
    public string? Value(string key) => TokenReplace.Parse(SetHelper.Design.Value(key, VPageLevel1)).EmptyAsNull();

    public IMagicPageSetSettings Settings => SetHelper.Settings;

    public IEnumerator<IMagicPageWithDesignWip> GetEnumerator() => Children.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}