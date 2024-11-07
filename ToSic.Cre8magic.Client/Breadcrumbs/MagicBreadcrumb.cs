using Oqtane.UI;
using ToSic.Cre8magic.Client.Breadcrumbs.Settings;
using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Breadcrumbs;

public class MagicBreadcrumb(PageState pageState) : MagicBreadcrumbItem(new(pageState))
{
    public MagicBreadcrumb(MagicSettings magicSettings) : this(magicSettings.PageState)
    {
        Helper.Set(magicSettings);
    }

    #region Init

    public MagicBreadcrumb Setup(MagicBreadcrumbSettings settings)
    {
        Helper.Set(settings);
        _currentPage = PageFactory.GetOrNull(settings.Start);
        return this;
    }

    public MagicBreadcrumb Designer(IBreadcrumbDesigner designer)
    {
        Helper.Set(designer);
        return this;
    }
    #endregion

    /// <summary>
    /// First page in the breadcrumb.
    /// Often the home page, but could be a different one.
    /// </summary>
    private MagicPage CurrentPage => _currentPage ??= PageFactory.Current;
    private MagicPage? _currentPage;

    public IEnumerable<MagicBreadcrumbItem> Items => _items ??= GetBreadcrumbs();
    private IEnumerable<MagicBreadcrumbItem>? _items;


    #region Private Methods

    private List<MagicBreadcrumbItem> GetBreadcrumbs(MagicPage? page = null)
    {
        var currentPage = page ?? CurrentPage;
        var breadcrumbs = new List<MagicBreadcrumbItem>
        {
            new (PageFactory, Helper, currentPage)
        };

        if (PageFactory.Home.PageId == currentPage.PageId)
            return breadcrumbs;

        // TODO: this is a bit strange, it appears that no matter what start page was set, the home-page will also be added
        breadcrumbs.Insert(0, new (PageFactory, Helper, PageFactory.Home));

        var oqtPages = PageFactory.PageState.Pages;
        var parentPage = oqtPages.FirstOrDefault(p => p.PageId == currentPage.ParentId);
        while (parentPage != null && PageFactory.Home.PageId != parentPage.PageId)
        {
            breadcrumbs.Insert(1, new (PageFactory, Helper, PageFactory.Create(parentPage)));
            parentPage = oqtPages.FirstOrDefault(p => p.PageId == parentPage.ParentId);
        }
        return breadcrumbs;
    } 
    #endregion
}