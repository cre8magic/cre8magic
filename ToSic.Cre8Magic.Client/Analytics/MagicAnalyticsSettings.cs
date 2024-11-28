using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Analytics;

/// <summary>
/// Settings to set up and use Google Tag Manager to track page views.
///
/// BETA: Doesn't completely work yet.
/// 
/// * Main caveat: Integration of the Google Tag Manager still has the GTM hardwired in the JS, must be finished.
/// * Background is that the Module currently doesn't contain its own JS, so it's still part of the theme.
/// </summary>
public record MagicAnalyticsSettings : MagicSettingsBase, ICanClone<MagicAnalyticsSettings>
{
    #region Constructor and Clone

    [PrivateApi]
    public MagicAnalyticsSettings() { }

    [PrivateApi]
    internal MagicAnalyticsSettings(MagicAnalyticsSettings? priority, MagicAnalyticsSettings? fallback = default)
        : base(priority, fallback)
    {
        GtmId = priority?.GtmId ?? fallback?.GtmId;
        PageViewTrack = priority?.PageViewTrack ?? fallback?.PageViewTrack;
        PageViewTrackFirst = priority?.PageViewTrackFirst ?? fallback?.PageViewTrackFirst;
        PageViewJs = priority?.PageViewJs ?? fallback?.PageViewJs;
        PageViewEvent = priority?.PageViewEvent ?? fallback?.PageViewEvent;
    }

    MagicAnalyticsSettings ICanClone<MagicAnalyticsSettings>.CloneUnder(MagicAnalyticsSettings? priority, bool forceCopy) => 
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion

    /// <summary>
    /// ID of Google Tag Manager.
    /// </summary>
    public string? GtmId { get; init; }

    public bool? PageViewTrack { get; init; }

    public bool? PageViewTrackFirst { get; init; }

    /// <summary>
    /// JavaScript function to call for tracking page views.
    /// Defaults to "gtag".
    /// </summary>
    public string? PageViewJs { get; init; }

    /// <summary>
    /// Name of the event to give to GTM which should be logged.
    /// Defaults to "blazor_page_view".
    /// </summary>
    public string? PageViewEvent { get; init; }


    internal static Defaults<MagicAnalyticsSettings> Defaults = new(new()
    {
        GtmId = null,
        PageViewTrack = false,
        PageViewTrackFirst = false,
        PageViewJs = "gtag",
        PageViewEvent = "blazor_page_view"
    });


}