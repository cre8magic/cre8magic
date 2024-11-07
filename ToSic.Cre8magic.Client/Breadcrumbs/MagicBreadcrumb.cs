using Oqtane.UI;
using ToSic.Cre8magic.Client.Breadcrumbs.Settings;
using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Breadcrumbs;

public class MagicBreadcrumb : MagicBreadcrumbItem
{
    public MagicBreadcrumb(PageState pageState) : base(new MagicPageFactory(pageState))
    {
        StartPage = PageFactory.Current;
        Settings = MagicBreadcrumbSettings.Defaults.Fallback;
        Design = new MagicBreadcrumbDesigner(this, Settings);
    }

    public MagicBreadcrumb(MagicSettings magicSettings) : this(magicSettings.PageState)
    {
        MagicSettings = magicSettings;
    }

    #region Init
    public MagicBreadcrumb Setup(MagicBreadcrumbSettings settings)
    {
        Settings = settings;
        StartPage = Settings.Start.HasValue
            ? PageFactory.GetOrNull(Settings.Start.Value) ?? PageFactory.Current
            : PageFactory.Current;
        return this;
    }

    public MagicBreadcrumb Designer(IBreadcrumbDesigner designer)
    {
        Design = designer;
        return this;
    }
    #endregion

    public MagicBreadcrumbSettings Settings { get; private set; }

    private MagicSettings? MagicSettings { get; }

    public MagicPage StartPage { get; private set; }

    public List<MagicBreadcrumbItem> Items => GetBreadcrumbs();
    //public List<MagicPage> Breadcrumbs => pageState.Breadcrumbs(_currentPage).ToList();

    internal IBreadcrumbDesigner Design { get; private set; }

    internal override MagicBreadcrumb Breadcrumb => this;

    internal TokenEngine PageTokenEngine(MagicPage page)
    {
        // fallback without MagicSettings return just TokenEngine with PageTokens
        if (MagicSettings == null)
            return new TokenEngine([new PageTokens(PageFactory, page)]);

        var originalPage = (PageTokens)MagicSettings.Tokens.Parsers.First(p => p.NameId == PageTokens.NameIdConstant);
        originalPage = originalPage.Clone(page);
        return MagicSettings.Tokens.SwapParser(originalPage);
    }

    #region Private Methods

    private List<MagicBreadcrumbItem> GetBreadcrumbs(MagicPage? page = null)
    {
        var currentPage = page ?? StartPage;
        var breadcrumbs = new List<MagicBreadcrumbItem>
        {
            new (PageFactory, currentPage, this)
        };

        if (PageFactory.Home.PageId == currentPage.PageId)
            return breadcrumbs;

        breadcrumbs.Insert(0, new (PageFactory, PageFactory.Home, this));

        var oqtPages = PageFactory.PageState.Pages;
        var parentPage = oqtPages.FirstOrDefault(p => p.PageId == currentPage.ParentId);
        while (parentPage != null && PageFactory.Home.PageId != parentPage.PageId)
        {
            breadcrumbs.Insert(1, new (PageFactory, PageFactory.Create(parentPage), this));
            parentPage = oqtPages.FirstOrDefault(p => p.PageId == parentPage.ParentId);
        }
        return breadcrumbs;
    } 
    #endregion
}