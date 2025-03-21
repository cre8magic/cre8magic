namespace ToSic.Cre8magic.Themes;

public interface IMagicThemeJsService
{
    Task Log(params object[] args);

    /// <summary>
    /// Clear the <body> tag of all classes
    /// </summary>
    /// <returns></returns>
    Task ClearBodyClasses();

    /// <summary>
    /// Set body classes (removes all previous classes in the process)
    /// </summary>
    /// <param name="classes"></param>
    /// <returns></returns>
    Task SetBodyClasses(string classes);

    ///// <summary>
    ///// Activate Google Tag Manager
    ///// </summary>
    ///// <param name="gtmId">GTM container ID, like 'GTM-XXXXXXX'</param>
    ///// <returns></returns>
    //Task GtmActivate(string gtmId);

    ///// <summary>
    ///// Track a page view in Google Tag Manager
    ///// </summary>
    ///// <returns></returns>
    //Task GtmPageView();
}