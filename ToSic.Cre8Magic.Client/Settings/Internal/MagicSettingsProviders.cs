using ToSic.Cre8magic.Breadcrumb;
using ToSic.Cre8magic.Containers;

namespace ToSic.Cre8magic.Settings.Internal;

internal class MagicSettingsProviders: IMagicSettingsProviders
{
    public MagicSettingsProviders()
    { }

    public MagicSettingsProvider<MagicContainerSettings> ContainerSettings =>
        _containerSettings ??= new MagicSettingsProvider<MagicContainerSettings>(this);
    private MagicSettingsProvider<MagicContainerSettings>? _containerSettings;

    public MagicSettingsProvider<MagicBreadcrumbSettings> BreadcrumbSettings =>
        _breadcrumbSettings ??= new MagicSettingsProvider<MagicBreadcrumbSettings>(this);
    private MagicSettingsProvider<MagicBreadcrumbSettings>? _breadcrumbSettings;

}