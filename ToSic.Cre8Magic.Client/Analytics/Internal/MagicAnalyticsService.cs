using Microsoft.JSInterop;
using Oqtane.UI;
using ToSic.Cre8magic.Themes.Settings;
using static ToSic.Cre8magic.Client.Services.DoStuff;

namespace ToSic.Cre8magic.Analytics;

public class MagicAnalyticsService(IJSRuntime jsRuntime, IMagicSettingsService settingsSvc) : IMagicAnalyticsService
{
    private const string GtmEvent = "event";

    public async Task TrackPage(PageState pageState, bool firstRender)
    {
        var themeContext = settingsSvc.GetThemeContext(pageState);
        var themeSettings = themeContext.ThemeSettings;
        var bestName = themeSettings.Parts.GetPartRenameOrFallback(nameof(IMagicSettingsService.Analytics), themeContext.SettingsName);
        var analyticsSettings = settingsSvc.Analytics.Find(bestName, themeContext.SettingsName);
        await TrackPage(analyticsSettings, firstRender);
    }

    /// <summary>
    /// Must run in OnAfterRenderAsync for now
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    public async Task TrackPage(MagicAnalyticsSettings? settings, bool firstRender)
    {
        if (settings == null) return;
        if (settings.PageViewTrack != true) return;

        if (firstRender && settings.PageViewTrackFirst != true) return;
        var js = settings.PageViewJs!;
        var eventName = settings.PageViewEvent;

        // Run the JS Command but don't wait for it
        // https://stackoverflow.com/questions/17805887/using-async-without-await
        await DoNotWait(() => IgnoreError(() => jsRuntime.InvokeVoidAsync(js, GtmEvent, eventName)));
    }
}