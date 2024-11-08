using Oqtane.UI;
using ToSic.Cre8magic.Client.Models;
using ToSic.Cre8magic.Client.Pages;

namespace ToSic.Cre8magic.Client.Menus;

public class MagicMenuTree : MagicMenuPage
{
    public MagicMenuTree(PageState pageState) : this(new MagicPageFactory(pageState))
    { }

    private MagicMenuTree(MagicPageFactory pageFactory) : base(pageFactory, new MagicMenuPageSetHelper(pageFactory), pageFactory.Current, 1)
    {
        Log = SetHelper.LogRoot.GetLog("Root");
        Log.A($"Start with PageState for Page:{PageId}; Level:1");

        // update dependent properties
        MenuPages = PageFactory.UserPages.ToList();
        Debug = [];
    }

    internal MagicMenuTree(MagicSettings magicSettings, MagicMenuSettings settings, IEnumerable<MagicPage>? menuPages = null, List<string>? messages = null) : this(magicSettings.PageState)
    {
        Log.A($"Start with MagicSettings for Page:{PageId}; Level:1");

        SetHelper.Set(magicSettings);
        ((MagicMenuPageSetHelper)SetHelper).Set(settings);
        if (menuPages != null) SetMenuPages(menuPages);
        if (messages != null) SetMessages(messages);
    }

    #region Init

    public MagicMenuTree Setup(MagicMenuSettings? settings)
    {
        Log.A($"Init MagicMenuSettings Start:{settings?.Start}; Level:{settings?.Level}");
        if (settings != null) 
            ((MagicMenuPageSetHelper)SetHelper).Set(settings);
        return this;
    }

    public MagicMenuTree SetMenuPages(IEnumerable<MagicPage> menuPages)
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
    internal IList<MagicPage> MenuPages { get; private set; }

    internal override MagicMenuTree Tree => this;

    //internal List<MagicPage> Breadcrumb => _breadcrumb ??= PageFactory.Breadcrumbs(this).ToList();
    //private List<MagicPage>? _breadcrumb;

    //public override string MenuId => _menuId ??= Settings?.MenuId ?? "error-menu-id";
    //private string? _menuId;

    public int Depth => _depth ??= Settings?.Depth ?? MagicMenuSettings.LevelDepthFallback;
    private int? _depth;

    public List<string> Debug { get; private set; }

    protected override List<MagicPage> GetChildPages() => _rootPages ??= GetRootPages();
    private List<MagicPage>? _rootPages;

    protected List<MagicPage> GetRootPages()
    {
        var l = Log.Fn<List<MagicPage>>($"{PageId}");
        // Give empty list if we shouldn't display it
        var result = new NodeRuleHelper(PageFactory, MenuPages, this, Settings, Log).GetRootPages();
        return l.Return(result);
    }

}