using ToSic.Cre8magic.Client.Models;
using ToSic.Cre8magic.Client.Pages;

namespace ToSic.Cre8magic.Client.Breadcrumbs;

public class MagicBreadcrumbItem : MagicPageWithDesign
{
    /// <param name="pageFactory"></param>
    /// <param name="helper">The helper - or null in the first breadcrumb item</param>
    /// <param name="page">The original page.</param>
    internal MagicBreadcrumbItem(MagicPageFactory pageFactory, MagicPageHelperBase? helper = null, MagicPage? page = null) : base(pageFactory, helper ?? new MagicBreadcrumbHelper(pageFactory), page)
    {
    }

    internal new MagicBreadcrumbHelper Helper => (MagicBreadcrumbHelper)base.Helper;

}