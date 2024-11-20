using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;

namespace ToSic.Cre8magic.Breadcrumb;

public interface IMagicBreadcrumbKit
{
    IMagicPageList Pages { get; init; }
    //MagicBreadcrumbSettings Settings { get; init; }

    //IMagicPage Home { get; init; }
}