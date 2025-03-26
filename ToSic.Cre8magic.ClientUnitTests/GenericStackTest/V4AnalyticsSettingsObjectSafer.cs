using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

public record V4AnalyticsSettingsObjectSafer
{
    [field: AllowNull, MaybeNull]
    public Dictionary<string, object> Values => field ??= new();

    /// <summary>
    /// ID of Google Tag Manager.
    /// </summary>
    public string GtmId
    {
        get => Values.GetThis("");
        init => Values.AddThis(value);
    }

    public bool PageViewTrack
    {
        get => Values.GetThis(false);
        init => Values.AddThis(value);
    }

    public bool PageViewTrackFirst
    {
        get => Values.GetThis(false);
        init => Values.AddThis(value);
    }


    /// <summary>
    /// JavaScript function to call for tracking page views.
    /// Defaults to "gtag".
    /// </summary>
    public string PageViewJs
    {
        get => Values.GetThis("");
        init => Values.AddThis(value);
    }

    /// <summary>
    /// Name of the event to give to GTM which should be logged.
    /// Defaults to "blazor_page_view".
    /// </summary>
    public string PageViewEvent
    {
        get => Values.GetThis("Pageview");
        init => Values.AddThis(value);
    }
}