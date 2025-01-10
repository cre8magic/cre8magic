using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Internal.Logging;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Will create a MenuTree based on the current pages information and settings.
/// </summary>
public class MagicMenuService(IMagicSettingsService settingsSvc): IMagicMenuService
{
    //public bool NoInheritSettingsWip { get; set; } = false;

    public IMagicMenuKit MenuKit(PageState pageState, MagicMenuSettings? settings = null)
    {
        var (newSettings, journal) = // NoInheritSettingsWip
            //? new(settings ?? new(), new())
            /*:*/ MergeSettings(pageState, settings);

        // Transfer Logs from Tree creation to the current log
        var logRoot = new LogRoot();
        logRoot.Add("tree-build", journal.Messages);

        // Page Factory with possibly reduced set of possible pages it can return
        var pageFactory = new MagicPageFactory(pageState, newSettings.PagesSource, logRoot: logRoot);

        // Create the Menu Context which is used in various places
        var context = new MagicMenuWorkContext
        {
            Tailor = newSettings.Tailor,
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
        var kit = new MagicMenuKit
        {
            Root = root,
            Settings = newSettings,
            WorkContext = context,
        };
        return kit;
    }

    private DataWithJournal<MagicMenuSettings> MergeSettings(PageState pageState, MagicMenuSettings? settings)
    {
        var getSettings = new GetSettings(settingsSvc, pageState, settings?.Name);
        var spell = getSettings.GetBest(settings, settingsSvc.Menus);
        var bluePrint = getSettings.GetBest(settings?.Blueprint, settingsSvc.MenuBlueprints, ThemePartSectionEnum.Design);
        return new(spell.Data with { Blueprint = bluePrint.Data }, spell.Journal.With(bluePrint.Journal));
    }
}
