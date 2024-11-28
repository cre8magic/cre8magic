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