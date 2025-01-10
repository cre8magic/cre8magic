using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbService(IMagicSettingsService settingsSvc) : IMagicBreadcrumbService
{
    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = null)
    {
        var (settingsFull, themePart, _) = MergeSettings(pageState, settings);

        var pageFactory = new MagicPageFactory(pageState);
        var show = themePart?.Show != false;
        var (pages, childrenFactory) = pageFactory.Breadcrumb.Get(settingsFull);

        var root = new MagicPage(new() /* fake page, just for providing classes / values to the root outside the menu */, pageFactory, childrenFactory)
        {
            IsVirtualRoot = true,
            MenuLevel = 0,
        };

        return new MagicBreadcrumbKit
        {
            Pages = pages,
            Settings = settingsFull,
            Show = show,
            Root = root,
        };
    }

    private Data2WithJournal<MagicBreadcrumbSettings, MagicThemePartSettings?> MergeSettings(
        PageState pageState, MagicBreadcrumbSettings? settings)
    {
        var getSettings = new GetSettings(settingsSvc, pageState, settings?.Name);
        var spell = getSettings.GetBest(settings, settingsSvc.Breadcrumbs);
        var bluePrint = getSettings.GetBest(settings?.Blueprint, settingsSvc.BreadcrumbBlueprints, ThemePartSectionEnum.Design);
        return new(spell.Data with { Blueprint = bluePrint.Data }, getSettings.Part, spell.Journal.With(bluePrint.Journal));
    }
}