using System.Diagnostics.CodeAnalysis;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages.Internal.PageDesign;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils.Logging;
using Log = ToSic.Cre8magic.Utils.Logging.Log;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Factory to create Magic Pages.
/// This is necessary, because the pages need certain properties which require other services to be available.
/// </summary>
public class MagicPageFactory(PageState pageState, IEnumerable<IMagicPage>? restrictPages = default, bool ignorePermissions = false, LogRoot? logRoot = default): IMagicPageChildrenFactory
{
    internal PageState PageState => pageState ?? throw new ArgumentNullException(nameof(pageState));

    [field: AllowNull, MaybeNull]
    private WorkContext WorkContext =>
        field ??= new() { LogRoot = logRoot ?? new(), PageFactory = this, TokenEngine = new() };

    // TODO: MAKE use context ? 
    [field: AllowNull, MaybeNull]
    internal Log Log => field ??= WorkContext.LogRoot.GetLog("pageFact");

    /// <summary>
    /// Helper to calculate URL and other page properties.
    /// </summary>
    [field: AllowNull, MaybeNull]
    internal MagicPageProperties PageProperties => field ??= new(this);

    private readonly Dictionary<Page, IMagicPage> _cache = new();

    public IMagicPage Create(Page page)
    {
        if (_cache.TryGetValue(page, out var magicPage))
            return magicPage;
        var newPage = new MagicPage(page, this, this);
        _cache[page] = newPage;
        return newPage;
    }

    public IMagicPage? CreateOrNull(Page? page) => page == null ? null : Create(page);

    [field: AllowNull, MaybeNull]
    public IMagicPage Home => field
        ??= Create(OqtanePages.Find(p => p.Path == "") ?? throw new("Home page not found, no page with empty path"));

    [field: AllowNull, MaybeNull]
    public IMagicPage Current => field
        ??= Create(PageState.Page ?? throw new("Current Page not found"));


    public IMagicPage? GetOrNull(int? id) => id == null ? null : CreateOrNull(OqtanePages.FirstOrDefault(p => p.PageId == id));

    public IEnumerable<IMagicPage> Get(IEnumerable<int> ids) =>
        OqtanePages.Where(p => ids.Contains(p.PageId)).Select(Create);

    public IEnumerable<IMagicPage> Get(IEnumerable<Page> pages) =>
        pages.Select(Create);

    /// <summary>
    /// List of all pages - even these which would currently not be shown in the menu.
    /// </summary>
    public IEnumerable<IMagicPage> PagesAll() => _all ??= PageState.Pages.Select(Create).ToList();
    private List<IMagicPage>? _all;  // internally use list, so any further ToList() will be optimized

    public IEnumerable<IMagicPage> PagesCurrent() => _currentPages ??= restrictPages?.ToList() ?? (ignorePermissions ? PagesAll() : PagesUser()).ToList();
    private List<IMagicPage>? _currentPages;

    [field: AllowNull, MaybeNull]
    private List<Page> OqtanePages => field ??= PageState.Pages.ToList();

    #region Menu Pages - these are all the pages which the current user is allowed to see

    /// <summary>
    /// Pages in the menu according to Oqtane pre-processing
    /// Should be limited to pages which should be in the menu, visible and permissions ok. 
    /// </summary>
    public IEnumerable<IMagicPage> PagesUser() => GetUserMenuPages();

    private IEnumerable<IMagicPage> GetUserMenuPages()
    {
        var securityLevel = int.MaxValue;
        foreach (var page in OqtanePages.Where(item => item.IsNavigation))
        {
            if (page.Level <= securityLevel && UserSecurity.IsAuthorized(PageState.User, PermissionNames.View, page.PermissionList))
            {
                securityLevel = int.MaxValue;
                yield return Create(page);
            }
            else if (securityLevel == int.MaxValue)
            {
                securityLevel = page.Level;
            }
        }
    }


    #endregion

    #region Breadcrumb

    [field: AllowNull, MaybeNull]
    internal MagicBreadcrumbBuilder Breadcrumb => field ??= new(WorkContext);

    #endregion


    #region ChildrenOf

    public List<IMagicPage> ChildrenOf(int pageId)
    {
        var l = Log.Fn<List<IMagicPage>>(pageId.ToString());
        var result = PagesCurrent().Where(p => p.ParentId == pageId).ToList();
        return l.Return(result, result.LogPageList());
    }

    public List<IMagicPage> ChildrenOf(IMagicPage page)
    {
        var result = ChildrenOf(page.Id);
        return page is MagicPage { NodeRule: not null } magicPage
            ? CloneWithNodeRule(result, magicPage.NodeRule)
            : result;
    }

    internal static List<IMagicPage> CloneWithNodeRule(List<IMagicPage> result, PagesPickRule pickRule) =>
        result
            .Cast<MagicPage>()
            .Select(mp => mp with { NodeRule = pickRule })
            .Cast<IMagicPage>()
            .ToList();

    public IPageDesignHelperWip PageDesignHelper(IMagicPage page) => new PageDesignHelperBlank();

    #endregion

    internal IMagicPage ErrPage(int id, string message) => new MagicPage(new() { PageId = id, Name = message }, this, this);
}