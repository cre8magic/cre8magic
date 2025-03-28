using Oqtane.UI;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbService(IMagicSettingsService settingsSvc) : IMagicBreadcrumbService
{
    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = null)
    {
        // Merge provided settings with global settings - including the blueprint
        var gs = new GetSettings(settingsSvc, pageState, settings?.Name);
        var (settingsFull, _) = gs.GetBestPair<MagicBreadcrumbSettings, MagicBreadcrumbBlueprint>(settings);

        // Check if global settings configure this part to be shown
        // If there is no configuration for this part, assume default which is show true
        var show = gs.Part?.Show != false;

        // Generate the breadcrumb pages according to configuration
        var pageFactory = new MagicPageFactory(pageState);
        var (pages, childrenFactory) = pageFactory.Breadcrumb.Get(settingsFull);

        // Create a fake root page for use in the main "root" Tailor
        // for providing classes / values to the root outside the menu
        var root = new MagicPage(new(), pageFactory, childrenFactory)
        {
            IsVirtualRoot = true,
            MenuLevel = 0,
        };

        // Return the fully prepared kit
        return new MagicBreadcrumbKit
        {
            Pages = pages,
            Settings = settingsFull,
            Show = show,
            Root = root,
            Tailor = root.Tailor,
        };
    }

}