using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Will create a MenuTree based on the current pages information and settings.
/// </summary>
public class MagicMenuService(IMagicSettingsService settingsSvc): IMagicMenuService
{
    private const string OptionalPrefix = "menu-";
    private const string DefaultPartName = "Menus";

    public bool NoInheritSettingsWip { get; set; } = false;

    public IMagicMenuKit MenuKit(PageState pageState, MagicMenuSettings? settings = null)
    {
        var (newSettings, journal) = NoInheritSettingsWip
            ? new(settings ?? new(), new())    // todo: magicMenuSettings.Default.Fallback
            : MergeSettings(pageState, settings);

        // Transfer Logs from Tree creation to the current log
        var logRoot = new LogRoot();
        logRoot.Add("tree-build", journal.Messages);

        // Page Factory with possibly reduced set of possible pages it can return
        var pageFactory = new MagicPageFactory(pageState, newSettings.Pages, logRoot: logRoot);

        // Create the Menu Context which is used in various places
        var context = new MagicMenuWorkContext
        {
            Designer = newSettings.Designer,
            LogRoot = logRoot,
            PageFactory = pageFactory,
            TokenEngine = settingsSvc.PageTokenEngine(pageState),
            Settings = newSettings,
        };

        var nodeFactory = new MagicMenuNodeFactory(context);
        var root = new MagicPage(new() /* fake page, just for providing classes / values to the root outside the menu */, pageFactory, nodeFactory)
        {
            IsVirtualRoot = true,
            MenuLevel = 0,
        };
        var design = new PageListDesignWip(root);
        var kit = new MagicMenuKit
        {
            Page = root,
            Settings = newSettings,
            WorkContext = context,
            Design = design
        };
        return kit;
    }

    private DataWithJournal<MagicMenuSettings> MergeSettings(PageState pageState, MagicMenuSettings? settings) =>
        settingsSvc.GetBestSettingsAndDesignSettings(
            pageState,
            settings,
            settingsSvc.Menus,
            settings?.DesignSettings,
            settingsSvc.MenuDesigns, 
            OptionalPrefix,
            DefaultPartName,
            finalize: (settingsData, designSettings) => settingsData with /*new(settingsData, settings)*/ { DesignSettings = designSettings });


    //private static List<IMagicPage> GetRootPages(MagicMenuContextWip context, MagicMenuNodeFactory nodeFactory)
    //{
    //    var log = context.LogRoot.GetLog("get-root");

    //    var pageFactory = context.PageFactory;
    //    var settings = context.Settings;
        
    //    // Add break-point for debugging during development
    //    if (pageFactory.PageState.IsDebug())
    //        pageFactory.PageState.DoNothing();

    //    var l = log.Fn<List<IMagicPage>>("Root");
    //    l.A($"Start with PageState for Page:{pageFactory.Current.Id}; Level:1");

    //    var levelsRemaining = nodeFactory.MaxDepth;
    //    if (levelsRemaining < 0)
    //        return l.Return([], "remaining levels 0 - return empty");

    //    var rootPages = new NodeRuleHelper(pageFactory, pageFactory.Current, settings, log).GetRootPages();
    //    l.A($"Root pages ({rootPages.Count}): {rootPages.LogPageList()}");

    //    var children = rootPages
    //        .Select(child => new MagicPageWithDesign(pageFactory, nodeFactory, child, 2 /* todo: should probably be 1 */) as IMagicPage)
    //        .ToList();
    //    return l.Return(children, $"{children.Count}");
    //}

}
