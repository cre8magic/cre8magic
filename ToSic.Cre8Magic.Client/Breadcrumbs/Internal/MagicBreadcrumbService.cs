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
    private const string DefaultPartName = "Breadcrumb";

    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = null)
    {
        var (settingsFull, _, themePart, _) = MergeSettings(pageState, settings);

        var factory = new MagicPageFactory(pageState);
        var show = themePart?.Show != false;
        var list = factory.Breadcrumb.Get(settingsFull);
        var design = new PageListDesignWip(list);
        return new MagicBreadcrumbKit
        {
            Pages = list,
            Settings = settingsFull,
            Show = show,
            Design = design
        };
    }

    private Data3WithJournal<MagicBreadcrumbSettings, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicBreadcrumbSettings? settings) =>
        settingsSvc.GetBestSettingsAndDesignSettings(
            pageState,
            settings,
            settingsSvc.Breadcrumbs,
            settings?.DesignSettings,
            settingsSvc.BreadcrumbDesigns,
            OptionalPrefix,
            DefaultPartName,
            finalize: (settingsData, designSettings) => settingsData with { DesignSettings = designSettings });

}