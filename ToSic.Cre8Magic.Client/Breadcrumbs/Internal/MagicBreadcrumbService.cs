using Oqtane.UI;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbService(IMagicSettingsService settingsSvc) : IMagicBreadcrumbService
{
    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = null)
    {
        var gs = new GetSettings(settingsSvc, pageState, settings?.Name);
        var (settingsFull, _) = gs.GetBestPair<MagicBreadcrumbSettings, MagicBreadcrumbBlueprint>(settings);

        var pageFactory = new MagicPageFactory(pageState);
        var show = gs.Part?.Show != false;
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
            Tailor = root.Tailor,
        };
    }

}