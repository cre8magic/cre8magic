using System.Diagnostics.CodeAnalysis;

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

public record V3AnalyticsSettingsObjectNonGeneric
{
    [field: AllowNull, MaybeNull]
    public V3ObjectDictionaryNonGeneric Meta => field ??= new();

    /// <summary>
    /// ID of Google Tag Manager.
    /// </summary>
    public string GtmId
    {
        get => Meta.GetThis("");
        init => Meta.AddThis(value);
    }

    public bool PageViewTrack
    {
        get => Meta.GetThis(false);
        init => Meta.AddThis(value);
    }

    public bool PageViewTrackFirst
    {
        get => Meta.GetThis(false);
        init => Meta.AddThis(value);
    }


    /// <summary>
    /// JavaScript function to call for tracking page views.
    /// Defaults to "gtag".
    /// </summary>
    public string PageViewJs
    {
        get => Meta.GetThis("");
        init => Meta.AddThis(value);
    }

    /// <summary>
    /// Name of the event to give to GTM which should be logged.
    /// Defaults to "blazor_page_view".
    /// </summary>
    public string PageViewEvent
    {
        get => Meta.GetThis("Pageview");
        init => Meta.AddThis(value);
    }
}