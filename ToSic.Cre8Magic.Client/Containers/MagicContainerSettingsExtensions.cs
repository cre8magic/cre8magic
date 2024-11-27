using Oqtane.Models;
using Oqtane.UI;

namespace ToSic.Cre8magic.Containers;

public static class MagicContainerSettingsExtensions
{
    public static MagicContainerSettings With(this MagicContainerSettings? settings, Module moduleState) =>
        (settings ?? new()) with
        {
            ModuleState = moduleState
        };

    public static MagicContainerSettings With(this MagicContainerSettings? settings, PageState pageState, Module moduleState) =>
        (settings ?? new()) with
        {
            ModuleState = moduleState,
            PageState = pageState,
        };

}