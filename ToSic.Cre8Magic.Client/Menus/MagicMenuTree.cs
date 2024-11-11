using Oqtane.UI;
using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Client.Pages.Internal;
using ToSic.Cre8magic.Pages;
using Log = ToSic.Cre8magic.Client.Logging.Log;

namespace ToSic.Cre8magic.Client.Menus;

public class MagicMenuTree : IMagicPageListOld
{
    public MagicMenuTree(PageState pageState, MagicMenuGetSpecsWip? specs = null)
    {
        Specs = specs ?? new();
        PageFactory = new(pageState, Specs.Pages);
        SetHelper = new(PageFactory, this);
        SetHelper.Set(Specs.MagicSettings);
        SetHelper.Set(Specs.Designer);
        SetHelper.Set(Specs.Settings);
        Log = SetHelper.LogRoot.GetLog("Root");
        Log.A($"Start with PageState for Page:{PageFactory.Current.Id}; Level:1");
    }

    internal MagicMenuGetSpecsWip Specs { get; }

    internal Log Log { get; }

    internal MagicMenuPageSetHelper SetHelper { get; }
    private MagicPageFactory PageFactory { get; }
    public MagicMenuSettings Settings => SetHelper.SettingsTyped;


    internal MagicMenuTree Tree => this;

    public int MaxDepth => _maxDepth ??= Specs.Depth ?? Settings?.Depth ?? MagicMenuSettings.LevelDepthFallback;
    private int? _maxDepth;

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

        var rootPages = new NodeRuleHelper(PageFactory, PageFactory.Current, Settings, Log).GetRootPages(Specs);
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(page => new MagicMenuPage(PageFactory, SetHelper, page, MenuLevel + 1))
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