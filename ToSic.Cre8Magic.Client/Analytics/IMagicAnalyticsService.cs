using Oqtane.UI;

namespace ToSic.Cre8magic.Analytics;

/// <summary>
/// Service to help with Google Analytics Tracking.
/// </summary>
public interface IMagicAnalyticsService
{
    /// <summary>
    /// Simplest way to track a page, assumes that the settings are injected from a Settings Source.
    ///
    /// Will take the PageState and - depending on the settings - track the view in Google Analytics.
    ///
    /// Must run in `OnAfterRenderAsync` for now
    /// </summary>
    /// <param name="pageState">The current PageState as provided by Oqtane.</param>
    /// <param name="isFirstRender"></param>
    /// <returns></returns>
    Task TrackPage(PageState pageState, bool isFirstRender);

    /// <summary>
    /// Must run in OnAfterRenderAsync for now
    /// </summary>
    /// <param name="pageState">The current PageState as provided by Oqtane.</param>
    /// <param name="isFirstRender"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    Task TrackPage(PageState pageState, bool isFirstRender, MagicAnalyticsSettings? settings);
}