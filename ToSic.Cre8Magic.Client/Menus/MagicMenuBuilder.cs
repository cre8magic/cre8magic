using Microsoft.Extensions.Logging;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Menus;

/// <summary>
/// Will create a MenuTree based on the current pages information and configuration
/// </summary>
public class MagicMenuBuilder(ILogger<MagicMenuBuilder> logger, IMagicPageService pageSvc) : MagicServiceWithSettingsBase
{
    public ILogger Logger { get; } = logger;

    private const string MenuSettingPrefix = "menu-";

    public IMagicPageList GetTree(MagicMenuSettings settings, List<IMagicPage> menuPages)
    {
        var settingsSvc = AllSettings!.Service;
        var messages = new List<string>();
        var (configName, configMessages) = settingsSvc.FindConfigName(settings.ConfigName, AllSettings.Name);
        messages.AddRange(configMessages);

        // Check if we have a name-remap to consider
        var menuConfig = AllSettings.ConfigurationName(configName);
        if (menuConfig == null && !configName.StartsWith(MenuSettingPrefix))
            menuConfig = AllSettings.ConfigurationName($"{MenuSettingPrefix}{configName}");

        var updatedName = menuConfig; // Settings.Theme.Menus.FindInvariant(configName);
        if (updatedName.HasValue())
        {
            configName = updatedName!;
            messages.Add($"updated config to '{configName}'");
        }

        // If the user didn't specify a config name in the Parameters or the config name
        // isn't contained in the json file the normal parameter are given to the service
        var menuSettings = settingsSvc.MenuSettings.Find(configName);
        settings = JsonMerger.Merge(settings, menuSettings, Logger);

        // See if we have a default configuration for CSS which should be applied
        var menuDesign = AllSettings.DesignName(configName);
        if (menuDesign == null && !configName.StartsWith(MenuSettingPrefix))
            menuDesign = AllSettings.DesignName($"{MenuSettingPrefix}{configName}");

        var designName = menuDesign;
        messages.Add($"Design name in config: '{designName}'");
        if (string.IsNullOrWhiteSpace(designName))
        {
            designName = configName;
            messages.Add($"Design set to '{designName}'");
        }

        // Usually there is no Design-object pre-filled, in which case we should
        // 1. try to find it in json
        // 2. use the one from the configuration
        if (settings.DesignSettings == null)
        {
            // Check various places where design could be configured by priority
            var designConfig = settingsSvc.MenuDesigns.Find(designName, AllSettings.Name);

            //config.DesignSettings = designConfig;
            settings = settings with { DesignSettings = designConfig };
        }
        else
            messages.Add("Design rules already set");

        settings = settings with
        {
            AllSettings = AllSettings,
            Pages = menuPages,
        };

        var result = pageSvc.Setup(AllSettings.PageState).GetMenuInternal(settings, messages);
        return result;
    }
}
