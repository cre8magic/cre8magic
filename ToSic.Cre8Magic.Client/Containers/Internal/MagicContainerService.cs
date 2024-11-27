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

        var designer = ContainerDesigner(pageState, module, settingsFull.DesignSettings!);
        return new MagicContainerKit
        {
            Designer = designer,
            Module = module,

            Settings = settingsFull,
        };
    }

    private Data3WithJournal<MagicContainerSettings, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicContainerSettings? settings) =>
        settingsSvc.GetBestSettingsAndDesignSettings(
            pageState,
            settings,
            settingsSvc.Containers,
            settings?.DesignSettings,
            settingsSvc.ContainerDesigns,
            OptionalPrefix,
            DefaultPartName,
            finalize: (settingsData, designSettings) => settingsData with /* new(settingsData, settings)*/ { DesignSettings = designSettings });




    private MagicContainerDesigner ContainerDesigner(PageState pageState, Module module, MagicContainerDesignSettings designSettings)
    {
        if (_containerDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;

        var designContext = settingsSvc.GetThemeContextFull(pageState);
        var container = new MagicContainerDesigner(designContext, module, designSettings);
        _containerDesigners[module.ModuleId] = container;
        return container;
    }
    private readonly Dictionary<int, MagicContainerDesigner> _containerDesigners = new();
}