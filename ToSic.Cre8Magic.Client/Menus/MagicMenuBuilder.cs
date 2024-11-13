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

    public IMagicPageList GetTree(MagicMenuSettings config, List<IMagicPage> menuPages)
    {
        var settingsSvc = GlobalSettings!.Service;
        var messages = new List<string>();
        var (configName, configMessages) = settingsSvc.FindConfigName(config.ConfigName, GlobalSettings.Name);
        messages.AddRange(configMessages);

        // Check if we have a name-remap to consider
        var menuConfig = GlobalSettings.ConfigurationName(configName);
        if (menuConfig == null && !configName.StartsWith(MenuSettingPrefix))
            menuConfig = GlobalSettings.ConfigurationName($"{MenuSettingPrefix}{configName}");

        var updatedName = menuConfig; // Settings.Theme.Menus.FindInvariant(configName);
        if (updatedName.HasValue())
        {
            configName = updatedName!;
            messages.Add($"updated config to '{configName}'");
        }

        // If the user didn't specify a config name in the Parameters or the config name
        // isn't contained in the json file the normal parameter are given to the service
        var menuSettings = settingsSvc.MenuSettings.Find(configName);
        config = JsonMerger.Merge(config, menuSettings, Logger);

        // See if we have a default configuration for CSS which should be applied
        var menuDesign = GlobalSettings.DesignName(configName);
        if (menuDesign == null && !configName.StartsWith(MenuSettingPrefix))
            menuDesign = GlobalSettings.DesignName($"{MenuSettingPrefix}{configName}");

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
        if (config.DesignSettings == null)
        {
            // Check various places where design could be configured by priority
            var designConfig = settingsSvc.MenuDesigns.Find(designName, GlobalSettings.Name);

            //config.DesignSettings = designConfig;
            config = config with { DesignSettings = designConfig };
        }
        else
            messages.Add("Design rules already set");

        config = config with
        {
            MagicSettings = GlobalSettings,
            Pages = menuPages,
            DebugMessages = messages,
        };

        return pageSvc.Setup(GlobalSettings.PageState).GetMenu(config);

        //return pageSvc.Setup(GlobalSettings.PageState)
        //    .GetMenu(new()
        //    {
        //        MagicSettings = GlobalSettings,
        //        Settings = config,
        //        Pages = menuPages,
        //        DebugMessages = messages
        //    });
    }
}
