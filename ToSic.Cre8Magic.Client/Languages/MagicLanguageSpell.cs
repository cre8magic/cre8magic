using System.Text.Json.Serialization;
using ToSic.Cre8magic.Internal.Json;
using ToSic.Cre8magic.Spells;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Languages;

public record MagicLanguageSpell : MagicSpellBase, ICanClone<MagicLanguageSpell>
{
    /// <summary>
    /// Dummy constructor so better find cases where it's created
    /// Note it must be without parameters for json deserialization
    /// </summary>
    [PrivateApi]
    public MagicLanguageSpell() {}

    private MagicLanguageSpell(MagicLanguageSpell? priority, MagicLanguageSpell? fallback = default)
        : base(priority, fallback)
    {
        HideOthers = priority?.HideOthers ?? fallback?.HideOthers;
        MinLanguagesToShow = SpellHelpers.PickFirstNonZeroInt([priority?.MinLanguagesToShow, fallback?.MinLanguagesToShow]);
        Languages = priority?.Languages ?? fallback?.Languages;

        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
    }

    MagicLanguageSpell ICanClone<MagicLanguageSpell>.CloneUnder(MagicLanguageSpell? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// If true, will only show the languages which are explicitly configured.
    /// If false, will first show the configured languages, then the rest. 
    /// </summary>
    public bool? HideOthers { get; init; }
    internal bool HideOthersSafe => HideOthers == true;

    public int MinLanguagesToShow { get; init; }

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


    internal static Defaults<MagicLanguageSpell> Defaults = new()
    {
        Fallback = new()
        {
            HideOthers = false,
            MinLanguagesToShow = 2,
            Languages = new()
            {
                { "en", new() { Culture = "en", Description = "English" } }
            },
        },
        Foundation = new()
        {
            HideOthers = false,
            Languages = new(),
        }
    };
}