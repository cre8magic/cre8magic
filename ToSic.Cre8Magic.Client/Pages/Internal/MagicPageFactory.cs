using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumb.Internal;
using ToSic.Cre8magic.Utils.Logging;
using Log = ToSic.Cre8magic.Utils.Logging.Log;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Factory to create Magic Pages.
/// This is necessary, because the pages need certain properties which require other services to be available.
/// </summary>
public class MagicPageFactory(PageState pageState, IEnumerable<IMagicPage>? restrictPages = default, bool ignorePermissions = false, LogRoot? logRoot = default)
{
    internal PageState PageState => pageState ?? throw new ArgumentNullException(nameof(pageState));

    // TODO: MAKE use context ? 
    internal Log Log => _log ??= (logRoot ?? new LogRoot()).GetLog("pageFact");
    private Log? _log;

    /// <summary>
    /// Helper to calculate URL and other page properties.
    /// </summary>
    internal MagicPageProperties PageProperties => _pageProperties ??= new(this);
    private MagicPageProperties? _pageProperties;

    private readonly Dictionary<Page, IMagicPage> _cache = new();

    public IMagicPage Create(Page page)
    {
        if (_cache.TryGetValue(page, out var magicPage))
            return magicPage;
        var newPage = new MagicPage(page, this);
        _cache[page] = newPage;
        return newPage;
    }

    public IMagicPage? CreateOrNull(Page? page) => page == null ? null : Create(page);

    public IMagicPage Home => _home ??= Create(OqtanePages.Find(p => p.Path == "") ?? throw new("Home page not found, no page with empty path"));
    private IMagicPage? _home;

    public IMagicPage Current => _current ??= Create(PageState.Page ?? throw new("Current Page not found"));
    private IMagicPage? _current;

    

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

    private List<Page> OqtanePages => _oqtanePages ??= PageState.Pages.ToList();
    private List<Page>? _oqtanePages;

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

    internal MagicBreadcrumbBuilder Breadcrumb => _breadcrumbFactory ??= new(this);
    private MagicBreadcrumbBuilder? _breadcrumbFactory;

    #endregion


    #region ChildrenOf

    public List<IMagicPage> ChildrenOf(int pageId)
    {
        var l = Log.Fn<List<IMagicPage>>(pageId.ToString());
        var result = PagesCurrent().Where(p => p.ParentId == pageId).ToList();
        return l.Return(result, result.LogPageList());
    }


    #endregion

    internal IMagicPage ErrPage(int id, string message) => new MagicPage(new() { PageId = id, Name = message }, this);
}