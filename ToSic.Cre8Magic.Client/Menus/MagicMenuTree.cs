//using System.Collections;
//using Oqtane.UI;
//using ToSic.Cre8magic.Client.Pages;
//using ToSic.Cre8magic.Client.Pages.Internal;
//using ToSic.Cre8magic.Client.Pages.Internal.Menu;
//using ToSic.Cre8magic.Pages;
//using Log = ToSic.Cre8magic.Client.Logging.Log;

//namespace ToSic.Cre8magic.Client.Menus;

//public class MagicMenuTree : IMagicPageList
//{
//    public MagicMenuTree(PageState pageState, MagicMenuGetSpecsWip specs)
//    {
//        Specs = specs;
//        FactoryRoot = new MagicMenuFactoryRoot(pageState, specs);
//        PageFactory = FactoryRoot.PageFactory;
//        FactorySub = FactoryRoot.Factory;
//        Log = FactorySub.LogRoot.GetLog("Root");
//        Log.A($"Start with PageState for Page:{PageFactory.Current.Id}; Level:1");
//    }

//    private MagicMenuFactoryRoot FactoryRoot { get; }

//    internal MagicMenuGetSpecsWip Specs { get; }

//    internal Log Log { get; }

//    private MagicMenuFactory FactorySub { get; }
//    private MagicPageFactory PageFactory { get; }

//    //MagicPagesFactoryBase IMagicPageListInternal.Factory2 => FactorySub;
//    MagicPagesFactoryBase IMagicPageList.Factory => FactorySub;

//    public int MenuLevel => 1;

//    //public bool HasChildren => Children.Any();

//    public IEnumerable<IMagicPageWithDesignWip> Children => _children ??= FactoryRoot.GetChildren();
//    private IList<IMagicPageWithDesignWip>? _children;
    

//    private ITokenReplace TokenReplace => _nodeReplace ??= FactorySub.PageTokenEngine(VPageLevel1);
//    private ITokenReplace? _nodeReplace;

//    private IMagicPage VPageLevel1 => _vPageLevel1 ??= new MagicPage(new() { Level = 0 /* Level is 0, so MenuLevel will be 1 */ }, PageFactory);
//    private IMagicPage? _vPageLevel1;
//    private MagicPagesFactoryBase _factory2;

//    /// <inheritdoc cref="IMagicPageList.Classes" />
//    public string? Classes(string tag) => TokenReplace.Parse(FactorySub.Design.Classes(tag, VPageLevel1)).EmptyAsNull();

//    /// <inheritdoc cref="IMagicPageList.Value" />
//    public string? Value(string key) => TokenReplace.Parse(FactorySub.Design.Value(key, VPageLevel1)).EmptyAsNull();

//    public IMagicPageSetSettings Settings => FactorySub.Settings;

//    public IEnumerator<IMagicPageWithDesignWip> GetEnumerator() => Children.GetEnumerator();

//    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

//}