using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.Utils.DoStuff;

namespace ToSic.Cre8magic.Analytics.Internal;

internal class MagicAnalyticsService(IMagicAnalyticsJsService analyticsJsService, IMagicSettingsService settingsSvc, ScopedDictionaryCache<bool> cacheSvc) : IMagicAnalyticsService
{
    private const string GtmEvent = "event";

    public IMagicAnalyticsKit AnalyticsKit(PageState pageState, MagicAnalyticsSettings? settings = null) =>
        BuildKit(pageState, settings);

    private MagicAnalyticsKit BuildKit(PageState pageState, MagicAnalyticsSettings? settings = null)
    {
        var (settingsData, _) = new GetSettings(settingsSvc, pageState, settings?.Name)
            .GetBest(settings, settingsSvc.Analytics);

        var result = new MagicAnalyticsKit
        {
            Settings = settingsData,
            PageState = pageState,
            Service = this
        };

        return result;
    }

    /// <summary>
    /// Call to do tracking, which will be accessed by the kit.
    /// </summary>
    /// <param name="pageState"></param>
    /// <param name="settings"></param>
    /// <param name="isFirstRender"></param>
    /// <returns></returns>
    internal async Task TrackPage(PageState pageState, MagicAnalyticsSettings? settings, bool isFirstRender)
    {
        // Check settings null on page view tracking disabled
        if (settings is not { PageViewTrack: true })
            return;
        if (isFirstRender && settings.PageViewTrackFirst != true) return;

        // Activate GTM ounce per user browser session (until reload)
        await GtmActivateOunce(settings.GtmId);

        // Run the JS Command but don't wait for it
        // https://stackoverflow.com/questions/17805887/using-async-without-await
        //await DoNotWait(() => IgnoreError(() => magicThemeJsService.Gtag(GtmEvent, settings?.PageViewEvent ?? "blazor_page_view")));
        await DoNotWait(() => IgnoreError(() => analyticsJsService.GtmPageView()));
    }

    /// <summary>
    /// Activate GTM ounce per user browser session (until reload)
    /// </summary>
    /// <param name="gtmId"></param>
    /// <returns></returns>
    private async Task GtmActivateOunce(string? gtmId)
    {
        if (string.IsNullOrWhiteSpace(gtmId) || cacheSvc.ContainsKey(gtmId))
            return;

        cacheSvc[gtmId] = true;
        await analyticsJsService.GtmAddToPage(gtmId);
    }
}