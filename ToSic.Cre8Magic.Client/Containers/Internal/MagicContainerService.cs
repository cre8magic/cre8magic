using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Containers.Internal;

internal class MagicContainerService(IMagicSettingsService settingsSvc) : IMagicContainerService
{
    public IMagicContainerKit ContainerKit(PageState pageState, Module module, MagicContainerSettings? settings = default)
    {
        var (settingsFull, journal) = MergeSettings(pageState, settings);

        var designer = ContainerTailor(pageState, module, settingsFull.Blueprint!);
        return new MagicContainerKit
        {
            Tailor = designer,
            Module = module,

            Settings = settingsFull,
        };
    }

    private DataWithJournal<MagicContainerSettings> MergeSettings(
        PageState pageState, MagicContainerSettings? settings)
    {

        var getSpell = new GetSpell(settingsSvc, pageState, settings?.Name);
        var spell = getSpell.GetBestSpell(settings, settingsSvc.Containers);
        var bluePrint = getSpell.GetBestSpell(settings?.Blueprint, settingsSvc.ContainerBlueprints, ThemePartSectionEnum.Design);
        return new(spell.Data with { Blueprint = bluePrint.Data }, spell.Journal.With(bluePrint.Journal));
    }


    private MagicContainerTailor ContainerTailor(PageState pageState, Module module, MagicContainerBlueprint blueprint)
    {
        var designContext = settingsSvc.GetThemeContextFull(pageState);
        var container = new MagicContainerTailor(designContext, module, blueprint);
        return container;
    }
}