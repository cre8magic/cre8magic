using Oqtane.UI;

namespace ToSic.Cre8magic.Analytics;

public interface IMagicAnalyticsService
{
    Task TrackPage(PageState pageState, bool firstRender);

    /// <summary>
    /// Must run in OnAfterRenderAsync for now
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    Task TrackPage(MagicAnalyticsSettings? settings, bool firstRender);
}