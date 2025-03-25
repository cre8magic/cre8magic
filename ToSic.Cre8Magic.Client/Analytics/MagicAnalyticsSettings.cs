using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
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
public record MagicAnalyticsSettings : MagicSettings, ICanClone<MagicAnalyticsSettings>
{
    #region Constructor and Clone

    [PrivateApi]
    public MagicAnalyticsSettings() { }

    [PrivateApi]
    private protected MagicAnalyticsSettings(MagicAnalyticsSettings? priority, MagicAnalyticsSettings? fallback = default)
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


    #region Internal Reader

    [PrivateApi]
    public Stabilized GetStable() => new(this);

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public new record Stabilized(MagicAnalyticsSettings AnalyticsSettings): MagicSettings.Stabilized(AnalyticsSettings)
    {
        public string GtmId => AnalyticsSettings.GtmId ?? DefaultGtmId;
        public const string DefaultGtmId = "gtm-id-undefined";

        public bool PageViewTrack => AnalyticsSettings.PageViewTrack ?? DefaultPageViewTrack;
        public const bool DefaultPageViewTrack = false;

        public bool PageViewTrackFirst => AnalyticsSettings.PageViewTrackFirst ?? DefaultPageViewTrackFirst;
        public const bool DefaultPageViewTrackFirst = false;

        public string PageViewJs => AnalyticsSettings.PageViewJs ?? DefaultPageViewJs;
        public const string DefaultPageViewJs = "gtag";

        public string PageViewEvent => AnalyticsSettings.PageViewEvent ?? DefaultPageViewEvent;
        public const string DefaultPageViewEvent = "blazor_page_view";
    }

    #endregion

}