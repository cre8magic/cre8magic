using Oqtane.UI;

namespace ToSic.Cre8magic.Analytics.Internal;

internal record MagicAnalyticsKit : IMagicAnalyticsKit
{
    public required MagicAnalyticsSettings Settings { get; init; }

    internal required PageState PageState { get; init; }

    internal required MagicAnalyticsService Service { get; init; }

    /// <summary>
    /// Simplest way to track a page, assumes that the settings are injected from a Settings Source.
    /// 
    /// Will take the PageState and - depending on the settings - track the view in Google Analytics.
    /// 
    /// Must run in `OnAfterRenderAsync` for now
    /// </summary>
    /// <param name="isFirstRender"></param>
    /// <returns></returns>
    public Task TrackPage(bool isFirstRender) => Service.TrackPage(PageState, Settings, isFirstRender);
}