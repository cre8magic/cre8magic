using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumbs;

public interface IMagicBreadcrumbKit
{
    IMagicPageList Pages { get; }
    MagicBreadcrumbSettings Settings { get; }

    bool Show { get; }
    IMagicDesign Design { get; init; }
}