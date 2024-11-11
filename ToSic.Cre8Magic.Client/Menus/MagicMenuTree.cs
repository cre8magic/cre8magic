using Oqtane.UI;
using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Client.Pages.Internal;
using ToSic.Cre8magic.Pages;
using Log = ToSic.Cre8magic.Client.Logging.Log;

namespace ToSic.Cre8magic.Client.Menus;

public class MagicMenuTree : IMagicPageListOld
{
    internal MagicMenuTree(MagicSettings magicSettings, MagicMenuSettings settings, IEnumerable<IMagicPage>? menuPages = null, List<string>? messages = null)
        : this(magicSettings.PageState)
    {
        SetHelper.Set(magicSettings);
        SetHelper.Set(settings);
        if (menuPages != null) SetMenuPages(menuPages);
        if (messages != null) SetMessages(messages);
    }

    public MagicMenuTree(PageState pageState) : this(new MagicPageFactory(pageState))
    { }

    private MagicMenuTree(MagicPageFactory pageFactory)
    {
        PageFactory = pageFactory;
        SetHelper = new(pageFactory);
        Log = SetHelper.LogRoot.GetLog("Root");
        Log.A($"Start with PageState for Page:{pageFactory.Current.Id}; Level:1");

        // update dependent properties
        MenuPages = PageFactory.GetUserPages().ToList();
        Debug = [];
    }

    internal Log Log { get; }

    internal MagicMenuPageSetHelper SetHelper { get; }
    internal MagicPageFactory PageFactory { get; }
    public MagicMenuSettings Settings => SetHelper.Settings;

    #region Init

    public MagicMenuTree Setup(MagicMenuSettings? settings)
    {
        Log.A($"Init MagicMenuSettings Start:{settings?.Start}; Level:{settings?.Level}");
        SetHelper.Set(settings);
        return this;
    }

    public MagicMenuTree SetMenuPages(IEnumerable<IMagicPage> menuPages)
    {
        Log.A($"Init menuPages:{menuPages.Count()}");
        MenuPages = menuPages.ToList();
        return this;
    }

    public MagicMenuTree SetMessages(List<string> messages)
    {
        Log.A($"Init messages:{messages.Count}");
        Debug = messages;
        return this;
    }

    public MagicMenuTree Designer(IPageDesigner pageDesigner)
    {
        Log.A($"Init MenuDesigner:{pageDesigner != null}");
        SetHelper.Set(pageDesigner);
        return this;
    }

    #endregion

    /// <summary>
    /// Pages in the menu according to Oqtane pre-processing
    /// Should be limited to pages which should be in the menu, visible and permissions ok. 
    /// </summary>
    internal IList<IMagicPage> MenuPages { get; private set; }

    internal /*override*/ MagicMenuTree Tree => this;

    public int MaxDepth => _maxDepth ??= Settings?.Depth ?? MagicMenuSettings.LevelDepthFallback;
    private int? _maxDepth;

    public List<string> Debug { get; private set; }

    public int MenuLevel => 1;

    public bool HasChildren => Children.Any();

    public IList<MagicMenuPage> Children => _children ??= GetChildren();
    private IList<MagicMenuPage>? _children;

    protected List<MagicMenuPage> GetChildren()
    {
        var l = Log.Fn<List<MagicMenuPage>>($"{nameof(MenuLevel)}: {MenuLevel}");
        var levelsRemaining = Tree.MaxDepth - (MenuLevel - 1 /* Level is 1 based, so -1 */);
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var rootPages = new NodeRuleHelper(PageFactory, MenuPages, PageFactory.Current, Settings, Log).GetRootPages();
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(page => new MagicMenuPage(PageFactory, SetHelper, page, MenuLevel + 1, Tree, $"{Log.Prefix}>{PageFactory.Current.Id}"))
            .ToList();
        return l.Return(children, $"{children.Count}");
    }



    private ITokenReplace TokenReplace => _nodeReplace ??= SetHelper.PageTokenEngine(VPageLevel1);
    private ITokenReplace? _nodeReplace;

    private IMagicPage VPageLevel1 => _vPageLevel1 ??= new MagicPage(new() { Level = 0 /* Level is 0, so MenuLevel will be 1 */ }, PageFactory);
    private IMagicPage? _vPageLevel1;

    /// <inheritdoc cref="IMagicPageListOld.Classes" />
    public string? Classes(string tag) => TokenReplace.Parse(SetHelper.Design.Classes(tag, VPageLevel1)).EmptyAsNull();

    /// <inheritdoc cref="IMagicPageListOld.Value" />
    public string? Value(string key) => TokenReplace.Parse(SetHelper.Design.Value(key, VPageLevel1)).EmptyAsNull();

}