using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Analytics;

public record MagicAnalyticsSettings : SettingsWithInherit
{
    // public NamedSettings<DesignSetting> Custom { get; set; } = new();

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