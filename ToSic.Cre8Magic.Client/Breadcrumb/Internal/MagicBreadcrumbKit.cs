using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumb;

internal record MagicBreadcrumbKit : IMagicBreadcrumbKit
{
    public required IMagicPageList Pages { get; init; }

    //public required IMagicPage Home { get; init; }

    //public required MagicBreadcrumbSettings Settings { get; init; }
}