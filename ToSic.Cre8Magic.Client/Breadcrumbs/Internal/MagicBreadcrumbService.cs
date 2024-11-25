using Oqtane.UI;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbService(IMagicSettingsService settingsSvc) : IMagicBreadcrumbService
{
    private const string OptionalPrefix = "breadcrumb-";
    private const string DefaultPartName = "Breadcrumbs";

    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettingsWip? settings = null)
    {
        var factory = new MagicPageFactory(pageState);
        var list = factory.Breadcrumb.Get(settings);
        return new MagicBreadcrumbKit
        {
            Pages = list,
            //Home = factory.Home,
            //Settings = list,
        };
    }

    private Data3WithJournal<MagicBreadcrumbSettingsWip, MagicThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicBreadcrumbSettingsWip? settings) =>
        settingsSvc.GetBestSettingsAndDesignSettings(
            pageState,
            settings,
            settingsSvc.Breadcrumbs,
            settings?.DesignSettings,
            settingsSvc.BreadcrumbDesigns,
            OptionalPrefix,
            DefaultPartName,
            finalize: (settingsData, designSettings) => new(settingsData, settings) { DesignSettings = designSettings });

}