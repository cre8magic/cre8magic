using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.MagicConstants;

namespace ToSic.Cre8magic.Themes;

public record MagicThemeSpell: MagicSpellBase, IHasDebugSettings, ICanClone<MagicThemeSpell>
{
    #region Constructor & Clone
    
    [PrivateApi]
    public MagicThemeSpell() { }

    private MagicThemeSpell(MagicThemeSpell? priority, MagicThemeSpell? fallback = default)
        : base(priority, fallback)
    {
        Logo = priority?.Logo ?? fallback?.Logo ?? Defaults.Fallback.Logo;

        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);

        Design = priority?.Design ?? fallback?.Design;
        Debug = priority?.Debug ?? fallback?.Debug;
    }

    MagicThemeSpell ICanClone<MagicThemeSpell>.CloneUnder(MagicThemeSpell? priority, bool forceCopy) =>
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
    public Dictionary<string, MagicThemePartSettings> Parts { get; init; } = new();

    public string? Design { get; init; }

    //public MagicDebugSettings? Debug { get; init; }

    internal static Defaults<MagicThemeSpell> Defaults = new(new()
    {
        Logo = "unknown-logo.png",
        Design = InheritName,
    });
}