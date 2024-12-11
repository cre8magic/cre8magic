using Microsoft.JSInterop;
using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using static ToSic.Cre8magic.Utils.DoStuff;

namespace ToSic.Cre8magic.Analytics.Internal;

public class MagicAnalyticsService(IJSRuntime jsRuntime, IMagicSpellsService spellsSvc) : IMagicAnalyticsService
{
    private const string OptionalPrefix = "analytics-";
    private const string DefaultPartName = "Analytics";

    private const string GtmEvent = "event";

    public IMagicAnalyticsKit AnalyticsKit(PageState pageState, MagicAnalyticsSpell? settings = null) =>
        BuildKit(pageState, settings);

    private MagicAnalyticsKit BuildKit(PageState pageState, MagicAnalyticsSpell? settings = null)
    {
        //var x = 7;
        //if ((settings as IDebugSettings)?.DebugThis == true)
        //    x = 8;

        var (settingsData, _, _, _) = MergeSettings(pageState, settings);
        //var settingsFull = new MagicAnalyticsSettings(settingsData, settings);

        var result = new MagicAnalyticsKit
        {
            Spell = settingsData,
            PageState = pageState,
            Service = this
        };

        return result;
    }

    private Data3WithJournal<MagicAnalyticsSpell, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicAnalyticsSpell? settings) =>
        spellsSvc.GetBestSpell(
            pageState,
            settings,
            spellsSvc.Analytics,
            OptionalPrefix,
            DefaultPartName
        );


    /// <summary>
    /// Call to do tracking, which will be accessed by the kit.
    /// </summary>
    /// <param name="pageState"></param>
    /// <param name="settings"></param>
    /// <param name="isFirstRender"></param>
    /// <returns></returns>
    internal async Task TrackPage(PageState pageState, MagicAnalyticsSpell? settings, bool isFirstRender)
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