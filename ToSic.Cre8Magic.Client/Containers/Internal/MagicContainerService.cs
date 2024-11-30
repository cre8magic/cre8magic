using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Containers.Internal;

internal class MagicContainerService(IMagicSettingsService settingsSvc) : IMagicContainerService
{
    private const string OptionalPrefix = "container-";
    private const string DefaultPartName = "Container";

    public IMagicContainerKit ContainerKit(PageState pageState, Module module, MagicContainerSettings? settings = default)
    {
        var (settingsFull, _, _, journal) = MergeSettings(pageState, settings);

        var designer = ContainerTailor(pageState, module, settingsFull.Blueprint!);
        return new MagicContainerKit
        {
            Tailor = designer,
            Module = module,

            Settings = settingsFull,
        };
    }

    private Data3WithJournal<MagicContainerSettings, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicContainerSettings? settings) =>
        settingsSvc.GetBestSettingsAndDesignSettings(
            pageState,
            settings,
            settingsSvc.Containers,
            settings?.Blueprint,
            settingsSvc.ContainerBlueprints,
            OptionalPrefix,
            DefaultPartName,
            finalize: (settingsData, designSettings) => settingsData with /* new(settingsData, settings)*/ { Blueprint = designSettings });




    private MagicContainerTailor ContainerTailor(PageState pageState, Module module, MagicContainerBlueprint blueprint)
    {
        if (_containerDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;

        var designContext = settingsSvc.GetThemeContextFull(pageState);
        var container = new MagicContainerTailor(designContext, module, blueprint);
        _containerDesigners[module.ModuleId] = container;
        return container;
    }
    private readonly Dictionary<int, MagicContainerTailor> _containerDesigners = new();
}