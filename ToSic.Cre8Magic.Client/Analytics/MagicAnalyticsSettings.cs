using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Analytics;

public record MagicAnalyticsSettings : SettingsWithInherit, ICanClone<MagicAnalyticsSettings>
{
    public MagicAnalyticsSettings()
    { }

    public MagicAnalyticsSettings(MagicAnalyticsSettings? priority, MagicAnalyticsSettings? fallback = default)
        : base(priority, fallback)
    {
        GtmId = priority?.GtmId ?? fallback?.GtmId;
        PageViewTrack = priority?.PageViewTrack ?? fallback?.PageViewTrack;
        PageViewTrackFirst = priority?.PageViewTrackFirst ?? fallback?.PageViewTrackFirst;
        PageViewJs = priority?.PageViewJs ?? fallback?.PageViewJs;
        PageViewEvent = priority?.PageViewEvent ?? fallback?.PageViewEvent;

    }

    public MagicAnalyticsSettings CloneMerge(MagicAnalyticsSettings? priority, bool forceCopy = false) => 
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    public string? GtmId { get; init; }

    public bool? PageViewTrack { get; init; }

    public bool? PageViewTrackFirst { get; init; }

    public string? PageViewJs { get; init; }

    public string? PageViewEvent { get; init; }


    private static readonly MagicAnalyticsSettings FbAndF = new()
    {
        GtmId = null,
        PageViewTrack = false,
        PageViewTrackFirst = false,
        PageViewJs = "gtag",
        PageViewEvent = "blazor_page_view"
    };

    internal static Defaults<MagicAnalyticsSettings> Defaults = new()
    {
        Fallback = FbAndF,
        Foundation = FbAndF,
    };


}