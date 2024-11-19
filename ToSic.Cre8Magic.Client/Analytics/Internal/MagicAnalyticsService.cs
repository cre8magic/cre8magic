using Microsoft.JSInterop;
using Oqtane.UI;
using ToSic.Cre8magic.Themes.Settings;
using static ToSic.Cre8magic.Client.Services.DoStuff;

namespace ToSic.Cre8magic.Analytics.Internal;

public class MagicAnalyticsService(IJSRuntime jsRuntime, IMagicSettingsService settingsSvc) : IMagicAnalyticsService
{
    private const string GtmEvent = "event";

    private MagicAnalyticsSettings GetAnalyticsSettings(PageState pageState)
    {
        var themeContext = settingsSvc.GetThemeContext(pageState);
        var themeSettings = themeContext.ThemeSettings;
        var bestName = themeSettings.Parts.GetPartRenameOrFallback(nameof(IMagicSettingsService.Analytics), themeContext.SettingsName);
        var analyticsSettings = settingsSvc.Analytics.Find(bestName, themeContext.SettingsName);
        return analyticsSettings;
    }

    /// <inheritdoc />
    public async Task TrackPage(PageState pageState, bool isFirstRender, MagicAnalyticsSettings? settings)
    {
        settings ??= GetAnalyticsSettings(pageState);
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