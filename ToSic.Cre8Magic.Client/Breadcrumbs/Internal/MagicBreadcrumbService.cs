using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
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
            Tailor =root.Tailor,
        };
    }

    private Data2WithJournal<MagicBreadcrumbSettings, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicBreadcrumbSettings? settings)
    {
        var getSettings = new GetSettings(settingsSvc, pageState, settings?.Name);
        var newSettings = getSettings.GetBest(settings, settingsSvc.Breadcrumbs);
        var blueprint = getSettings.GetBest(settings?.Blueprint, settingsSvc.BreadcrumbBlueprints, ThemePartSectionEnum.Design);
        return new(newSettings.Data with { Blueprint = blueprint.Data }, getSettings.Part, newSettings.Journal.With(blueprint.Journal));
    }

    public Data2WithJournal<TSettings, MagicThemePartSettings?> GetBestPair<TSettings, TBlueprint>(PageState pageState, TSettings? settings)
        where TSettings : MagicSettings, IWith<TBlueprint?>, new()
        where TBlueprint: class, new()
    {
        // Get / Merge Settings Helper
        var getSettings = new GetSettings(settingsSvc, pageState, settings?.Name);

        // Get best settings
        var settingsReader = settingsSvc.GetReader<TSettings>();
        var newSettings = getSettings.GetBest(settings, settingsReader);

        var blueprintReader = settingsSvc.GetReader<TBlueprint>();
        var blueprint = getSettings.GetBest(
            // The Blueprint accessed through the IWith interface
            (settings as IWith<TBlueprint>)?.WithData,
            blueprintReader,
            ThemePartSectionEnum.Design
        );

        var mergedWithBlueprint = newSettings.Data.With(blueprint.Data);

        return new(mergedWithBlueprint, getSettings.Part, newSettings.Journal.With(blueprint.Journal));
    }
}