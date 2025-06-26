namespace ToSic.Cre8magic.Analytics;

public interface IMagicAnalyticsJsService
{
    /// <summary>
    /// Activate Google Tag Manager
    /// </summary>
    /// <param name="gtmId">GTM container ID, like 'GTM-XXXXXXX'</param>
    /// <returns></returns>
    Task GtmAddToPage(string gtmId);

    /// <summary>
    /// Track a page view in Google Tag Manager
    /// </summary>
    /// <returns></returns>
    Task GtmPageView(string verb);

    /// <summary>
    /// Track a Google Analytics event
    /// </summary>
    /// <param name="target">'event'</param>
    /// <param name="more">'blazor_page_view'</param>
    /// <returns></returns>
    Task Gtag(string target, string more);
}