using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

public interface IMagicSettingsProvider
{
    MagicSettingsProviderSource<MagicContainerSettings> ContainerSettings { get; }
}