using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.Utils.DoStuff;

namespace ToSic.Cre8magic.Analytics.Internal;

internal class MagicAnalyticsService(IMagicAnalyticsJsService analyticsJsService, IMagicSettingsService settingsSvc, ScopedDictionaryCache<bool> cacheSvc) : IMagicAnalyticsService
{
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
        if (settings is null)
            return;
        var stable = settings.GetStable();
        if (!stable.PageViewTrack)
            return;
        if (isFirstRender && !stable.PageViewTrackFirst)
            return;

        // Activate GTM once per user browser session (until reload)
        // Note that it is timing-resistant, so it doesn't matter if this is executed
        // after the page tracking, since it will pick up the queue.
        await DoNotWaitAndIgnoreErrors(() => _ = GtmActivateOunce(stable.GtmId));

        // Run the JS Command but don't wait for it
        await DoNotWaitAndIgnoreErrors(() => analyticsJsService.GtmPageView(stable.PageViewEvent));
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