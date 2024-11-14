using Microsoft.Extensions.Logging;
using Oqtane.UI;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal.Menu;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Will create a MenuTree based on the current pages information and configuration
/// </summary>
public class MagicMenuService(ILogger<MagicMenuService> logger, IMagicSettingsService settingsSvc): IMagicMenuService
{
    public ILogger Logger { get; } = logger;

    private const string MenuSettingPrefix = "menu-";

    public bool NoInheritSettingsWip { get; set; } = false;

    public IMagicPageList GetMenu(PageState pageState, MagicMenuSettings? settings = default)
    {
        var (newSettings, messages) = NoInheritSettingsWip
        // todo: magicMenuSettings.Default.Fallback
            ? (settings ?? new(), new List<string>())
            : MergeSettings(pageState, settings);

        // Add break-point for debugging during development
        if (pageState.IsDebug()) pageState.DoNothing();

        var rootBuilder = new MagicMenuFactoryRoot(pageState, newSettings, messages);
        var list = new MagicPageList(new(pageState), rootBuilder.Factory, rootBuilder.GetChildren());
        return list;
    }

    private (MagicMenuSettings Settings, List<string> Messages) MergeSettings(PageState pageState, MagicMenuSettings? settings = default)
    {
        var messages = new List<string>();
        var allSettings = settingsSvc.GetSettings(pageState);
        var (configName, configMessages) = settingsSvc.FindConfigName(settings?.ConfigName, allSettings.Name);
        messages.AddRange(configMessages);

        // Check if we have a name-remap to consider
        var menuConfig = allSettings.ConfigurationName(configName);
        if (menuConfig == null && !configName.StartsWith(MenuSettingPrefix))
            menuConfig = allSettings.ConfigurationName($"{MenuSettingPrefix}{configName}");

        if (menuConfig.HasValue())
        {
            configName = menuConfig;
            messages.Add($"updated config to '{configName}'");
        }

        // If the user didn't specify a config name in the Parameters or the config name
        // isn't contained in the json file the normal parameter are given to the service
        var menuSettings = settingsSvc.MenuSettings.Find(configName);
        // WIP #DropJsonMerge
        //settings = JsonMerger.Merge(settings, menuSettings, Logger);
        var mergedSettings = menuSettings.CloneWith(settings);

        // See if we have a default configuration for CSS which should be applied
        var menuDesign = allSettings.DesignName(configName);
        if (menuDesign == null && !configName.StartsWith(MenuSettingPrefix))
            menuDesign = allSettings.DesignName($"{MenuSettingPrefix}{configName}");

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
        if (mergedSettings.DesignSettings == null)
        {
            // Check various places where design could be configured by priority
            var designConfig = settingsSvc.MenuDesigns.Find(designName, allSettings.Name);

            //config.DesignSettings = designConfig;
            mergedSettings = mergedSettings with { DesignSettings = designConfig };
        }
        else
            messages.Add("Design rules already set");

        mergedSettings = mergedSettings with
        {
            AllSettings = allSettings,
        };

        return (mergedSettings, messages);
    }

}
