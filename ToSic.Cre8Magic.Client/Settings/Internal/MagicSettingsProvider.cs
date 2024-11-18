using ToSic.Cre8magic.Containers;

namespace ToSic.Cre8magic.Settings.Internal;

internal class MagicSettingsProvider: IMagicSettingsProvider
{
    public MagicSettingsProvider()
    { }

    public MagicSettingsProviderSource<MagicContainerSettings> ContainerSettings =>
        _containerSettings ??= new(this);
    private MagicSettingsProviderSource<MagicContainerSettings>? _containerSettings;

}