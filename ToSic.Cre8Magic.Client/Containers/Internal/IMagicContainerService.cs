using Oqtane.Models;
using Oqtane.UI;

namespace ToSic.Cre8magic.Containers.Internal;

public interface IMagicContainerService
{
    IMagicContainerKit ContainerKit(PageState pageState, Module module, MagicContainerSettingsWip? settings = default);
}