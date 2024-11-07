using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using Oqtane.UI;
using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Services;

/// <summary>
/// Factory to create Magic Pages.
/// This is necessary, because the pages need certain properties which require other services to be available.
/// </summary>
public class MagicPageFactory(PageState pageState)
{
    internal PageState PageState => pageState;

    internal MagicPageHelpers PageHelpers => _pageHelpers ??= new(this);
    private MagicPageHelpers? _pageHelpers;

    public MagicPage Create(Page page) => new(page, this);

    public MagicPage? CreateOrNull(Page? page) => page == null ? null : new(page, this);

    public MagicPage Home => _home ??= Create(pageState.Pages.Find(p => p.Path == "") ?? throw new("Home page not found, no page with empty path"));

    private MagicPage? _home;

    public MagicPage Current => _current ??= Create(pageState.Page ?? throw new("Current Page not found"));

    private MagicPage? _current;

    public MagicPage? GetOrNull(int? id) => id == null ? null : CreateOrNull(pageState.Pages.FirstOrDefault(p => p.PageId == id));

    public IEnumerable<MagicPage> Get(IEnumerable<int> ids) =>
        pageState.Pages.Where(p => ids.Contains(p.PageId)).Select(Create);

    public IEnumerable<MagicPage> Get(IEnumerable<Page> pages) =>
        pages.Select(Create);

    public IEnumerable<MagicPage> All() => _all ??= pageState.Pages.Select(Create).ToList();
    private List<MagicPage>? _all;  // internally use list, so any further ToList() will be optimized

    #region Menu Pages - these are all the pages which the current user is allowed to see

    /// <summary>
    /// Pages in the menu according to Oqtane pre-processing
    /// Should be limited to pages which should be in the menu, visible and permissions ok. 
    /// </summary>
    public IEnumerable<MagicPage> MenuPages => GetMenuPages();

    private IEnumerable<MagicPage> GetMenuPages()
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

    #region Ancestors

    internal List<MagicPage> Breadcrumbs(MagicPage? page = null)
        => GetAncestors(page).Reverse().ToList();

    internal List<MagicPage> Breadcrumbs(List<MagicPage> pages, MagicPage page)
        => GetAncestors(pages, page).Reverse().ToList();

    internal List<MagicPage> Ancestors(MagicPage? page = null)
        => GetAncestors(page).ToList();

    private IEnumerable<MagicPage> GetAncestors(MagicPage? page = null)
        => GetAncestors(All().ToList(), page ?? Current);


    internal IEnumerable<MagicPage> GetAncestors(List<MagicPage> pages, MagicPage? page)
    {
        while (page != null)
        {
            yield return page;
            page = pages.FirstOrDefault(p => p.PageId == page.ParentId);
        }
    }


    #endregion
}