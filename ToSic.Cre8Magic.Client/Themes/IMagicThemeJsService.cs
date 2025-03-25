namespace ToSic.Cre8magic.Themes;

public interface IMagicThemeJsService
{
    /// <summary>
    /// Log to the console using `console.log(...)`
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Task Log(params object[] args);

    /// <summary>
    /// Clear the `&lt;body&gt;` tag of all classes
    /// </summary>
    /// <returns></returns>
    Task ClearBodyClasses();

    /// <summary>
    /// Set body classes (removes all previous classes in the process)
    /// </summary>
    /// <param name="classes"></param>
    /// <returns></returns>
    Task SetBodyClasses(string classes);

    /// <summary>
    /// Activate Google Tag Manager
    /// </summary>
    /// <param name="gtmId">GTM container ID, like 'GTM-XXXXXXX'</param>
    /// <returns></returns>
    Task GtmActivate(string gtmId);

    /// <summary>
    /// Track a page view in Google Tag Manager
    /// </summary>
    /// <returns></returns>
    Task GtmPageView();

    /// <summary>
    /// Track a Google Analytics event
    /// </summary>
    /// <param name="target">'event'</param>
    /// <param name="more">'blazor_page_view'</param>
    /// <returns></returns>
    Task Gtag(string target, string more);
}