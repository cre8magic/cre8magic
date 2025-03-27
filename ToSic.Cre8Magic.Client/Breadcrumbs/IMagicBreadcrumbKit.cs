using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Tailors;

namespace ToSic.Cre8magic.Breadcrumbs;

public interface IMagicBreadcrumbKit
{
    /// <summary>
    /// Pages to show in the breadcrumb, as specified by the settings.
    /// </summary>
    IEnumerable<IMagicPage> Pages { get; }

    /// <summary>
    /// Virtual "root" page of the breadcrumb, mainly for styling things around the real breadcrumb.
    /// </summary>
    IMagicPage Root { get; }

    internal MagicBreadcrumbSettings Settings { get; }

    /// <summary>
    /// Information if this Breadcrumb should be shown according to configuration.
    /// The code must decide if it respects this or not.
    /// </summary>
    bool Show { get; }

    IMagicTailor Tailor { get; }
}