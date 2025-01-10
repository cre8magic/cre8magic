using Microsoft.JSInterop;
using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using static ToSic.Cre8magic.Utils.DoStuff;

namespace ToSic.Cre8magic.Analytics.Internal;

public class MagicAnalyticsService(IJSRuntime jsRuntime, IMagicSettingsService settingsSvc) : IMagicAnalyticsService
{
    private const string GtmEvent = "event";

    public IMagicAnalyticsKit AnalyticsKit(PageState pageState, MagicAnalyticsSettings? settings = null) =>
        BuildKit(pageState, settings);

    private MagicAnalyticsKit BuildKit(PageState pageState, MagicAnalyticsSettings? settings = null)
    {
        var (settingsData, _) = new GetSpell(settingsSvc, pageState, settings?.Name)
            .GetBestSpell(settings, settingsSvc.Analytics);

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
        if (settings == null) return;
        if (settings.PageViewTrack != true) return;

        if (isFirstRender && settings.PageViewTrackFirst != true) return;
        var js = settings.PageViewJs!;
        var eventName = settings.PageViewEvent;

        // Run the JS Command but don't wait for it
        // https://stackoverflow.com/questions/17805887/using-async-without-await
        await DoNotWait(() => IgnoreError(() => jsRuntime.InvokeVoidAsync(js, GtmEvent, eventName)));
    }
}