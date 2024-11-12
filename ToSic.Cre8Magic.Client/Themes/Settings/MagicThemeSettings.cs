using ToSic.Cre8magic.Tokens;
using static ToSic.Cre8magic.Client.MagicConstants;

namespace ToSic.Cre8magic.Client.Themes.Settings;

public record MagicThemeSettings: SettingsWithInherit, IHasDebugSettings
{
    /// <summary>
    /// The logo to show, should be located in the assets subfolder
    /// </summary>
    public string? Logo { get; set; }

    public int LanguagesMin { get; init; }

    //public string? GtmId { get; set; }

    //public bool? GtmTrackPageView { get; set; }
    //public bool? GtmTrackPageViewFirst { get; set; }

    //public string? GtmTrackPageViewJs { get; set; }

    //public string? GtmTrackPageViewEvent { get; set; }

    /// <summary>
    /// The parts of this theme, like breadcrumb and various menu configs
    /// </summary>
    public NamedSettings<MagicThemePartSettings> Parts { get; init; } = new();


    public bool? MagicContextInBody { get; init; }

    public string? Design { get; init; }

    internal MagicThemeSettings Parse(ITokenReplace tokens)
    {
        Logo = tokens.Parse(Logo);
        return this;
    }

    public static MagicThemeSettings Fallback = new()
    {
        Logo = "unknown-logo.png",
        LanguagesMin = 2,
        MagicContextInBody = false,
        Design = InheritName,
    };

    internal static Defaults<MagicThemeSettings> Defaults = new()
    {
        Fallback = Fallback,
        Foundation = Fallback
    };

    public MagicDebugSettings? Debug { get; init; }
}