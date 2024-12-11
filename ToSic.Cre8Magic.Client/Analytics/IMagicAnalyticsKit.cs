namespace ToSic.Cre8magic.Analytics;

public interface IMagicAnalyticsKit
{
    MagicAnalyticsSpell Spell { get; init; }

    /// <summary>
    /// Simplest way to track a page, assumes that the settings are injected from a Settings Source.
    /// 
    /// Will take the PageState and - depending on the settings - track the view in Google Analytics.
    /// 
    /// Must run in `OnAfterRenderAsync` for now
    /// </summary>
    /// <param name="isFirstRender"></param>
    /// <returns></returns>
    Task TrackPage(bool isFirstRender);
}