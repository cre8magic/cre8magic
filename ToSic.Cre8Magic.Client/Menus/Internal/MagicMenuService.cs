using Microsoft.Extensions.Logging;
using Oqtane.UI;
using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Services.Internal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Will create a MenuTree based on the current pages information and configuration
/// </summary>
public class MagicMenuService(ILogger<MagicMenuService> logger, IMagicSettingsService settingsSvc): IMagicMenuService
{
    public ILogger Logger { get; } = logger;

    private const string MenuSettingPrefix = "menu-";

    public bool NoInheritSettingsWip { get; set; } = false;

    public IMagicMenuKit MenuKit(PageState pageState, MagicMenuSettings? settings = null)
    {
        var (newSettings, journal) = NoInheritSettingsWip
        // todo: magicMenuSettings.Default.Fallback
            ? (settings ?? new(), new List<string>())
            : MergeSettings(pageState, settings);

        // Transfer Logs from Tree creation to the current log
        var logRoot = new LogRoot();
        if (journal.Any())
        {
            var messageLog = logRoot.GetLog("tree-build");
            foreach (var m in journal) messageLog.A(m);
        }

        // Add break-point for debugging during development
        if (pageState.IsDebug())
            pageState.DoNothing();

        // Page Factory with possibly reduced set of possible pages it can return
        var pageFactory = new MagicPageFactory(pageState, newSettings.Pages, logRoot: logRoot);

        // Create the Menu Context which is used in various places
        var context = new MagicMenuContextWip
        {
            Designer = newSettings.Designer,
            LogRoot = logRoot,
            PageFactory = pageFactory,
            TokenEngineWip = settingsSvc.PageTokenEngine(pageState),
            Settings = newSettings,
        };

        var nodeFactory = new MagicMenuNodeFactory(context);
        var list = new MagicPageList(pageFactory, nodeFactory, GetRootPages(context, nodeFactory));

        var kit = new MagicMenuKit
        {
            Pages = list,
            Settings = newSettings,
            Context = context,
        };
        return kit;
    }

    private (MagicMenuSettings Settings, List<string> Journal) MergeSettings(PageState pageState, MagicMenuSettings? settings)
    {
        var (mainName, settingsName, designName, journal) = settingsSvc
            .GetMostRelevantNames(pageState, settings?.PartName, MenuSettingPrefix);

        // If the user didn't specify a config name in the Parameters or the config name
        // isn't contained in the json file the normal parameter are given to the service
        var mergedSettings = settingsSvc.MenuSettings.FindAndMerge(settingsName, mainName, settings);

        // Usually there is no Design-object pre-filled, in which case we should
        // 1. try to find it in json
        // 2. use the one from the configuration
        var designSettings = settingsSvc.MenuDesigns.FindAndMerge(designName, mainName, settings?.DesignSettings);

        var fullSettings = new MagicMenuSettings(ancestor: mergedSettings, settings)
        {
            DesignSettings = designSettings
        };

        return (fullSettings, journal);
    }


    private List<IMagicPageWithDesignWip> GetRootPages(MagicMenuContextWip context, MagicMenuNodeFactory nodeFactory)
    {
        var pageFactory = context.PageFactory;
        var log = context.LogRoot.GetLog("root");

        log.A($"Start with PageState for Page:{pageFactory.Current.Id}; Level:1");
        var settings = context.Settings;

        // Add break-point for debugging during development
        if (pageFactory.PageState.IsDebug())
            pageFactory.PageState.DoNothing();

        var l = log.Fn<List<IMagicPageWithDesignWip>>("Root");
        var levelsRemaining = nodeFactory.MaxDepth;
        if (levelsRemaining < 0)
            return l.Return([], "remaining levels 0 - return empty");

        var rootPages = new NodeRuleHelper(pageFactory, pageFactory.Current, settings, log).GetRootPages();
        l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

        var children = rootPages
            .Select(child => new MagicPageWithDesign(pageFactory, nodeFactory, child, 2 /* todo: should probably be 1 */) as IMagicPageWithDesignWip)
            .ToList();
        return l.Return(children, $"{children.Count}");
    }

}
