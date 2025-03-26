using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Internal.Json;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

public record MagicLanguageSettings : MagicSettings, ICanClone<MagicLanguageSettings>
{
    /// <summary>
    /// Dummy constructor so better find cases where it's created
    /// Note it must be without parameters for json deserialization
    /// </summary>
    [PrivateApi]
    public MagicLanguageSettings() {}

    private MagicLanguageSettings(MagicLanguageSettings? priority, MagicLanguageSettings? fallback = default)
        : base(priority, fallback)
    {
        HideOthers = priority?.HideOthers ?? fallback?.HideOthers;
        MinLanguagesToShow = SettingHelpers.PickFirstNonZeroInt([priority?.MinLanguagesToShow, fallback?.MinLanguagesToShow]);
        Languages = priority?.Languages ?? fallback?.Languages;

        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
    }

    MagicLanguageSettings ICanClone<MagicLanguageSettings>.CloneUnder(MagicLanguageSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// If true, will only show the languages which are explicitly configured.
    /// If false, will first show the configured languages, then the rest. 
    /// </summary>
    public bool? HideOthers { get; init; }

    /// <summary>
    /// Minimum Languages to auto-show this - typically / default is 2.
    /// </summary>
    public int? MinLanguagesToShow { get; init; }

    /// <summary>
    /// List of languages
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicLanguage>))]
    // ReSharper disable once RedundantAccessorBody
    public Dictionary<string, MagicLanguage>? Languages { get => field; init => field = InitList(value); }

    private static Dictionary<string, MagicLanguage>? InitList(Dictionary<string, MagicLanguage>? dic)
    {
        // Ensure each config knows what culture it's for, as
        var extended = dic?.ToDictionary(
            pair => pair.Key,
            pair => pair.Value with { Culture = pair.Key },
            StringComparer.InvariantCultureIgnoreCase
        );

        return extended;
    }

    [JsonIgnore]
    public MagicLanguageBlueprint? Blueprint { get; init; }

    #region Stabilized

    [PrivateApi]
    public Stabilized GetStable() => (_stabilized ??= new(new(this))).Value;
    private IgnoreEquals<Stabilized>? _stabilized;

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public new record Stabilized(MagicLanguageSettings LanguageSettings) : MagicSettings.Stabilized(LanguageSettings)
    {
        public bool HideOthers => LanguageSettings.HideOthers ?? DefaultHideOthers;
        public const bool DefaultHideOthers = false;

        public int MinLanguagesToShow => LanguageSettings.MinLanguagesToShow ?? DefaultMinLanguagesToShow;
        public const int DefaultMinLanguagesToShow = 2;

        [field: AllowNull, MaybeNull]
        public Dictionary<string, MagicLanguage> Languages => field ??= LanguageSettings.Languages ?? new();

        [field: AllowNull, MaybeNull]
        public MagicLanguageBlueprint Blueprint => field ??= LanguageSettings.Blueprint ?? new();
    }


    #endregion
}