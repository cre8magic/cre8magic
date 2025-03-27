using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Tailors;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal record MagicBreadcrumbKit : IMagicBreadcrumbKit
{
    public required IEnumerable<IMagicPage> Pages { get; init; }

    public required MagicBreadcrumbSettings Settings { get; init; }

    public required bool Show { get; init; }

    public required IMagicPage Root { get; init; }

    [field: AllowNull, MaybeNull]
    public IMagicTailor Tailor => field ??= ((MagicPage)Root).Tailor;
}