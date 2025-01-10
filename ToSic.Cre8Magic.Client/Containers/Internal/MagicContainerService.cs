using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Containers.Internal;

internal class MagicContainerService(IMagicSpellsService spellsSvc) : IMagicContainerService
{
    public IMagicContainerKit ContainerKit(PageState pageState, Module module, MagicContainerSpell? settings = default)
    {
        var (settingsFull, journal) = MergeSettings(pageState, settings);

        var designer = ContainerTailor(pageState, module, settingsFull.Blueprint!);
        return new MagicContainerKit
        {
            Tailor = designer,
            Module = module,

            Spell = settingsFull,
        };
    }

    private DataWithJournal<MagicContainerSpell> MergeSettings(
        PageState pageState, MagicContainerSpell? settings)
    {

        var getSpell = new GetSpell(spellsSvc, pageState, settings?.Name);
        var spell = getSpell.GetBestSpell(settings, spellsSvc.Containers);
        var bluePrint = getSpell.GetBestSpell(settings?.Blueprint, spellsSvc.ContainerBlueprints, ThemePartSectionEnum.Design);
        return new(spell.Data with { Blueprint = bluePrint.Data }, spell.Journal.With(bluePrint.Journal));
    }


    private MagicContainerTailor ContainerTailor(PageState pageState, Module module, MagicContainerBlueprint blueprint)
    {
        var designContext = spellsSvc.GetThemeContextFull(pageState);
        var container = new MagicContainerTailor(designContext, module, blueprint);
        return container;
    }
}