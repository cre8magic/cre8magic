using Oqtane.UI;
using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Docs;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Will create a MenuTree based on the current pages information and configuration
/// </summary>
public class MagicMenuService(IMagicSettingsService settingsSvc): IMagicMenuService
{
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
        logRoot.Add("tree-build", journal);

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
        // Get most relevant names
        var ((mainName, settingsName, designName), journal) = settingsSvc
            .GetMostRelevantNames(pageState, settings?.PartName, MenuSettingPrefix);

        // If the user didn't specify a config name in the Parameters or the config name
        // isn't contained in the json file the normal parameter are given to the service
        var themeCtx = settingsSvc.GetThemeContext(pageState);
        var findSettings = new FindSettingsSpecs(themeCtx, settings, ThemePartSectionEnum.Settings, MenuSettingPrefix);
        var (mergedSettings, settingsJournal) = settingsSvc.MenuSettings.FindAndMerge(findSettings, settings);

        // Usually there is no Design-object pre-filled, in which case we should
        // 1. try to find it in json
        // 2. use the one from the configuration
        findSettings = new(themeCtx, settings, ThemePartSectionEnum.Design, MenuSettingPrefix);
        var (designSettings, designJournal) = settingsSvc.MenuDesigns.FindAndMerge(findSettings, settings?.DesignSettings);

        var fullSettings = new MagicMenuSettings(ancestor: mergedSettings, settings)
        {
            DesignSettings = designSettings,
        };

        return (fullSettings, settingsJournal.Concat(designJournal).ToList());
    }


    private static List<IMagicPageWithDesignWip> GetRootPages(MagicMenuContextWip context, MagicMenuNodeFactory nodeFactory)
    {
        var log = context.LogRoot.GetLog("get-root");

        var pageFactory = context.PageFactory;
        var settings = context.Settings;
        
        // Add break-point for debugging during development
        if (pageFactory.PageState.IsDebug())
            pageFactory.PageState.DoNothing();

        var l = log.Fn<List<IMagicPageWithDesignWip>>("Root");
        l.A($"Start with PageState for Page:{pageFactory.Current.Id}; Level:1");

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
