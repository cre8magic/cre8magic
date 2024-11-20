using Microsoft.JSInterop;
using Oqtane.UI;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.Client.Services.DoStuff;

namespace ToSic.Cre8magic.Analytics.Internal;

public class MagicAnalyticsService(IJSRuntime jsRuntime, IMagicSettingsService settingsSvc) : IMagicAnalyticsService
{
    private const string GtmEvent = "event";

    public IMagicAnalyticsKit AnalyticsKit(PageState pageState, MagicAnalyticsSettings? settings = default) =>
        _cache.Get(pageState, () => new MagicAnalyticsKit
        {
            Settings = settings ?? GetAnalyticsSettings(pageState),
            PageState = pageState,
            Service = this
        });

    private readonly GetKeepByPageId<IMagicAnalyticsKit> _cache = new();

    private MagicAnalyticsSettings GetAnalyticsSettings(PageState pageState)
    {
        var themeContext = settingsSvc.GetThemeContext(pageState);
        var themeSettings = themeContext.ThemeSettings;
        var bestName = themeSettings.Parts.GetPartSettingsNameOrFallback(nameof(IMagicSettingsService.Analytics), themeContext.SettingsName);
        var analyticsSettings = settingsSvc.Analytics.FindAndNeutralize(bestName, themeContext.SettingsName);
        return analyticsSettings;
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