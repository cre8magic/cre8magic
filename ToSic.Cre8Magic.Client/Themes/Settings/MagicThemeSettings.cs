using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Tokens;
using static ToSic.Cre8magic.Client.MagicConstants;

namespace ToSic.Cre8magic.Client.Themes.Settings;

public record MagicThemeSettings: SettingsWithInherit, IHasDebugSettings, ICanClone<MagicThemeSettings>
{
    public MagicThemeSettings() { }

    public MagicThemeSettings(MagicThemeSettings? priority, MagicThemeSettings? fallback = default)
        : base(priority, fallback)
    {
        Logo = priority?.Logo ?? fallback?.Logo ?? Defaults.Fallback.Logo;
        LanguagesMin = priority?.LanguagesMin ?? fallback?.LanguagesMin ?? Defaults.Fallback.LanguagesMin;

        // TODO: #NamedSettings
        Parts = priority?.Parts ?? fallback?.Parts ?? new();

        MagicContextInBody = priority?.MagicContextInBody ?? fallback?.MagicContextInBody ?? Defaults.Fallback.MagicContextInBody;
        Design = priority?.Design ?? fallback?.Design ?? Defaults.Fallback.Design;
        Debug = priority?.Debug ?? fallback?.Debug;
    }

    public MagicThemeSettings CloneWith(MagicThemeSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// The logo to show, should be located in the assets subfolder
    /// </summary>
    public string? Logo { get; set; }

    public int LanguagesMin { get; init; }

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

    internal static Defaults<MagicThemeSettings> Defaults = new(new()
    {
        Logo = "unknown-logo.png",
        LanguagesMin = 2,
        MagicContextInBody = false,
        Design = InheritName,
    });

    public MagicDebugSettings? Debug { get; init; }
}