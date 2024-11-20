using Oqtane.UI;
using ToSic.Cre8magic.Pages.Internal;

namespace ToSic.Cre8magic.Breadcrumb.Internal;

internal class MagicBreadcrumbService : IMagicBreadcrumbService
{
    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = default)
    {
        var factory = new MagicPageFactory(pageState);
        var list = factory.Breadcrumb.Get(settings);
        return new MagicBreadcrumbKit
        {
            Pages = list,
            //Home = factory.Home,
            //Settings = list,
        };
    }
}