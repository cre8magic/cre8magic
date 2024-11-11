using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using Oqtane.UI;
using ToSic.Cre8magic.Pages;
using Log = ToSic.Cre8magic.Client.Logging.Log;

// using Log = Oqtane.Models.Log;

namespace ToSic.Cre8magic.Client.Pages.Internal;

/// <summary>
/// Factory to create Magic Pages.
/// This is necessary, because the pages need certain properties which require other services to be available.
/// </summary>
public class MagicPageFactory(PageState pageState)
{
    internal Log Log = new LogRoot().GetLog("pageFact");

    internal PageState PageState => pageState;

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

    public IMagicPage Home => _home ??= Create(pageState.Pages.Find(p => p.Path == "") ?? throw new("Home page not found, no page with empty path"));
    private IMagicPage? _home;

    public IMagicPage Current => _current ??= Create(pageState.Page ?? throw new("Current Page not found"));
    private IMagicPage? _current;

    

    public IMagicPage? GetOrNull(int? id) => id == null ? null : CreateOrNull(pageState.Pages.FirstOrDefault(p => p.PageId == id));

    public IEnumerable<IMagicPage> Get(IEnumerable<int> ids) =>
        pageState.Pages.Where(p => ids.Contains(p.PageId)).Select(Create);

    public IEnumerable<IMagicPage> Get(IEnumerable<Page> pages) =>
        pages.Select(Create);

    /// <summary>
    /// List of all pages - even these which would currently not be shown in the menu.
    /// </summary>
    public IEnumerable<IMagicPage> All() => _all ??= pageState.Pages.Select(Create).ToList();
    private List<IMagicPage>? _all;  // internally use list, so any further ToList() will be optimized

    #region Menu Pages - these are all the pages which the current user is allowed to see

    /// <summary>
    /// Pages in the menu according to Oqtane pre-processing
    /// Should be limited to pages which should be in the menu, visible and permissions ok. 
    /// </summary>
    public IEnumerable<IMagicPage> GetUserPages() => GetMenuPages();

    private IEnumerable<IMagicPage> GetMenuPages()
    {
        if (pageState == null)
            throw new InvalidOperationException("PageState is null.");

        var securityLevel = int.MaxValue;
        foreach (var page in pageState.Pages.Where(item => item.IsNavigation))
        {
            if (page.Level <= securityLevel && UserSecurity.IsAuthorized(pageState.User, PermissionNames.View, page.PermissionList))
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

    internal MagicBreadcrumbFactory Breadcrumb => _breadcrumbFactory ??= new(this);
    private MagicBreadcrumbFactory? _breadcrumbFactory;


    //internal IEnumerable<IMagicPage> Breadcrumb(IMagicPage? page = null)
    //    => GetBreadcrumb(null, ((_, magicPage) => magicPage), page);
    //    //=> Breadcrumb(All().ToList(), page);

    //internal IEnumerable<IMagicPage> Breadcrumb(IList<IMagicPage> pages, IMagicPage? page)
    //    => GetBreadcrumb(new() { Pages = pages }, ((_, magicPage) => magicPage), page);
    ////=> GetAncestors(pages, page ?? Current).Reverse().ToList();

    //internal IEnumerable<TPage> GetBreadcrumb<TPage>(MagicBreadcrumbGetSpecsWip? specs, Func<MagicPageFactory, IMagicPage, TPage> generator, IMagicPage? page = null)
    //{
    //    specs ??= new();
    //    var endPage = page ?? Current;
    //    // Create a new list with the current page
    //    var list = new List<TPage>();

    //    if (specs.WithCurrent)
    //        list.Add(generator(this, endPage));

    //    // If we are on home, exit now.
    //    var homeId = Home.Id;
    //    if (homeId == endPage.Id)
    //        return list;

    //    // Technically home is not in the breadcrumb, it's usually just the first page in the list
    //    if (specs.WithHome)
    //        list.Insert(0, generator(this, Home));

    //    // determine if we restrict the output list
    //    // Note that as of 2024-11-10 it has not been tested.
    //    var restrictions = specs.Pages?.Select(p => p.Id).ToHashSet();

    //    //// Find first parent page
    //    //var oqtPages = (specs.Pages ?? PageState.Pages).ToList();
    //    //var parentPage = oqtPages.FirstOrDefault(p => p.PageId == endPage.ParentId);
    //    var parentPage = endPage.Parent;

    //    // Loop through all parent pages until we reach the home page
    //    while (parentPage != null && homeId != parentPage.Id && list.Count <= specs.MaxDepth)
    //    {
    //        // Check if not in the list of restrictions
    //        if (restrictions != null && !restrictions.Contains(parentPage.Id))
    //            break;

    //        // Add to list
    //        list.Insert(1, generator(this, parentPage));
    //        // Find next parent
    //        parentPage = parentPage.Parent; // oqtPages.FirstOrDefault(p => p.PageId == parentPage.ParentId);
    //    }

    //    if (specs.Reverse)
    //        list.Reverse();

    //    return list;
    //}


    #endregion
    #region Ancestors

    //internal List<IMagicPage> Ancestors(IMagicPage? page = null)
    //    => GetAncestors(page).ToList();

    //private IEnumerable<IMagicPage> GetAncestors(IMagicPage? page = null)
    //    => GetAncestors(All().ToList(), page ?? Current);


    //internal IEnumerable<IMagicPage> GetAncestors(IList<IMagicPage> pages, IMagicPage? page)
    //{
    //    while (page != null)
    //    {
    //        yield return page;
    //        page = pages.FirstOrDefault(p => p.Id == page.ParentId);
    //    }
    //}


    #endregion

    #region ChildrenOf

    public List<IMagicPage> ChildrenOf(IList<IMagicPage> list, int pageId)
    {
        var l = Log.Fn<List<IMagicPage>>(pageId.ToString());
        var result = list.Where(p => p.ParentId == pageId).ToList();
        return l.Return(result, result.LogPageList());
    }


    #endregion

    internal IMagicPage ErrPage(int id, string message) => new MagicPage(new() { PageId = id, Name = message }, this);
}