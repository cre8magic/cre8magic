using Oqtane.UI;

namespace ToSic.Cre8magic.Breadcrumb;

public interface IMagicBreadcrumbService
{
    IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = default);
}