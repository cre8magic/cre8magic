using System.Diagnostics.CodeAnalysis;

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

public record V1AnalyticsSettingsMeta
{
    [field: AllowNull, MaybeNull]
    private V1MetaDictionary V1Meta => field ??= new();

    /// <summary>
    /// ID of Google Tag Manager.
    /// </summary>
    public string GtmId
    {
        get => V1Meta.GetThis("");
        init => V1Meta.AddThis(value);
    }

    public bool PageViewTrack
    {
        get => V1Meta.GetThis(false);
        init => V1Meta.AddThis(value);
    }

    public bool PageViewTrackFirst
    {
        get => V1Meta.GetThis(false);
        init => V1Meta.AddThis(value);
    }


    /// <summary>
    /// JavaScript function to call for tracking page views.
    /// Defaults to "gtag".
    /// </summary>
    public string PageViewJs
    {
        get => V1Meta.GetThis("");
        init => V1Meta.AddThis(value);
    }

    /// <summary>
    /// Name of the event to give to GTM which should be logged.
    /// Defaults to "blazor_page_view".
    /// </summary>
    public string PageViewEvent
    {
        get => V1Meta.GetThis("Pageview");
        init => V1Meta.AddThis(value);
    }
}