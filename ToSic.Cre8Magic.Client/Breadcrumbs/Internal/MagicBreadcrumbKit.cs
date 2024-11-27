using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal record MagicBreadcrumbKit : IMagicBreadcrumbKit
{
    public required IMagicPageList Pages { get; init; }

    public required MagicBreadcrumbSettings Settings { get; init; }

    public required bool Show { get; init; }

    public required IMagicDesign Design { get; init; }
}