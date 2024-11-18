using Microsoft.JSInterop;
using static ToSic.Cre8magic.Client.Services.DoStuff;

namespace ToSic.Cre8magic.Analytics;

public class MagicAnalyticsService(IJSRuntime jsRuntime)
{
    private const string GtmEvent = "event";
    public IJSRuntime JsRuntime { get; } = jsRuntime;

    /// <summary>
    /// Must run in OnAfterRenderAsync for now
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    internal async Task TrackPage(MagicAnalyticsSettings? settings, bool firstRender)
    {
        if (settings == null) return;
        if (settings.PageViewTrack != true) return;

        if (firstRender && settings.PageViewTrackFirst != true) return;
        var js = settings.PageViewJs!;
        var eventName = settings.PageViewEvent;

        // Run the JS Command but don't wait for it
        // https://stackoverflow.com/questions/17805887/using-async-without-await
        await DoNotWait(() => IgnoreError(() => JsRuntime.InvokeVoidAsync(js, GtmEvent, eventName)));
    }
}