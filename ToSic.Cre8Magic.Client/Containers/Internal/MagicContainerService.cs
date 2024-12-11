using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Containers.Internal;

internal class MagicContainerService(IMagicSpellsService spellsSvc) : IMagicContainerService
{
    private const string OptionalPrefix = "container-";
    private const string DefaultPartName = "Container";

    public IMagicContainerKit ContainerKit(PageState pageState, Module module, MagicContainerSpell? settings = default)
    {
        var (settingsFull, _, _, journal) = MergeSettings(pageState, settings);

        var designer = ContainerTailor(pageState, module, settingsFull.Blueprint!);
        return new MagicContainerKit
        {
            Tailor = designer,
            Module = module,

            Spell = settingsFull,
        };
    }

    private Data3WithJournal<MagicContainerSpell, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicContainerSpell? settings) =>
        spellsSvc.GetBestSettingsAndDesignSettings(
            pageState,
            settings,
            spellsSvc.Containers,
            settings?.Blueprint,
            spellsSvc.ContainerBlueprints,
            OptionalPrefix,
            DefaultPartName,
            finalize: (settingsData, designSettings) => settingsData with /* new(settingsData, settings)*/ { Blueprint = designSettings });




    private MagicContainerTailor ContainerTailor(PageState pageState, Module module, MagicContainerBlueprint blueprint)
    {
        var designContext = spellsSvc.GetThemeContextFull(pageState);
        var container = new MagicContainerTailor(designContext, module, blueprint);
        return container;
    }
}