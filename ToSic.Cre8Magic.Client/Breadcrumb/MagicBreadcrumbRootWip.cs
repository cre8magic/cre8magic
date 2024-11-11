using Oqtane.UI;
using ToSic.Cre8magic.Client.Pages;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Breadcrumb;

public class MagicBreadcrumbRootWip(PageState pageState) : MagicBreadcrumbItem(new(pageState))
{
    public MagicBreadcrumbRootWip(MagicSettings magicSettings) : this(magicSettings.PageState)
    {
        SetHelper.Set(magicSettings);
    }

    #region Init
    
    public MagicBreadcrumbRootWip Setup(MagicBreadcrumbGetSpecsWip? specs)
    {
        if (specs == null)
            return this;

        GetSpecs = specs;

        if (specs.Settings != null)
            SetHelper.Set(specs.Settings);

        if (specs.Designer != null)
            SetHelper.Set(specs.Designer);

        return this;
    }

    private MagicBreadcrumbGetSpecsWip GetSpecs { get; set; }

    #endregion

    /// <summary>
    /// First page in the breadcrumb.
    /// Often the home page, but could be a different one.
    /// </summary>
    private IMagicPage CurrentPage => _currentPage ??= PageFactory.Current;
    private IMagicPage? _currentPage;

    public IEnumerable<MagicBreadcrumbItem> Items => _items ??= PageFactory.Breadcrumb.Get(GetSpecs, (factory, magicPage) => new MagicBreadcrumbItem(factory, SetHelper, magicPage));
    private IEnumerable<MagicBreadcrumbItem>? _items;

}