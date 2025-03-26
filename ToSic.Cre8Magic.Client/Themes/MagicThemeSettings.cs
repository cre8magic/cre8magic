using System.Text.Json.Serialization;
using ToSic.Cre8magic.Internal.Json;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes;

public record MagicThemeSettings: MagicSettings, IHasDebugSettings, ICanClone<MagicThemeSettings>
{
    #region Constructor & Clone
    
    [PrivateApi]
    public MagicThemeSettings() { }

    private MagicThemeSettings(MagicThemeSettings? priority, MagicThemeSettings? fallback = default)
        : base(priority, fallback)
    {
        Logo = priority?.Logo ?? fallback?.Logo;

        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicThemeSettings ICanClone<MagicThemeSettings>.CloneUnder(MagicThemeSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion

    /// <summary>
    /// The logo to show, should be located in the assets subfolder
    /// </summary>
    public string? Logo { get; init; }

    /// <summary>
    /// The parts of this theme, like breadcrumb and various menu configs
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicThemePartSettings>))]
    public Dictionary<string, MagicThemePartSettings>? Parts { get; init; }

    #region Stabilized

    [PrivateApi]
    public Stabilized GetStable() => new(this);

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public new record Stabilized(MagicThemeSettings ThemeSettings) : MagicSettings.Stabilized(ThemeSettings)
    {
        public string Logo => ThemeSettings.Logo ?? DefaultLogo;
        public const string DefaultLogo = "unknown-logo.png";

        public Dictionary<string, MagicThemePartSettings> Parts => ThemeSettings.Parts ?? new();

    }


    #endregion
}