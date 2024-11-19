using ToSic.Cre8magic.Breadcrumb;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Special provider to retrieve settings.
///
/// It is scoped, so anything added to it - typically in the Theme,
/// will be available in all other components.
/// </summary>
public interface IMagicSettingsProviders
{
    MagicSettingsProvider<MagicContainerSettings> ContainerSettings { get; }

    MagicSettingsProvider<MagicBreadcrumbSettings> BreadcrumbSettings { get; }
}