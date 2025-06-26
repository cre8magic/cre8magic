using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Containers.Internal;

internal class MagicContainerService(IMagicSettingsService settingsSvc) : IMagicContainerService
{
    public IMagicContainerKit ContainerKit(PageState pageState, Module module, MagicContainerSettings? settings = default)
    {
        var gs = new GetSettings(settingsSvc, pageState, settings?.Name);
        var (settingsFull, _) = gs.GetBestPair<MagicContainerSettings, MagicContainerBlueprint>(settings);

        var designer = ContainerTailor(pageState, module, settingsFull.Blueprint!);
        return new MagicContainerKit
        {
            Tailor = designer,
            Module = module,

            Settings = settingsFull,
        };
    }


    private MagicContainerTailor ContainerTailor(PageState pageState, Module module, MagicContainerBlueprint blueprint)
    {
        var designContext = settingsSvc.GetThemeContextFull(pageState);
        var container = new MagicContainerTailor(designContext, module, blueprint);
        return container;
    }
}