using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

/// <summary>
/// Language Design Settings
/// </summary>
public record MagicLanguageBlueprint : SettingsWithInherit, ICanClone<MagicLanguageBlueprint>
{
    [PrivateApi]
    public MagicLanguageBlueprint() { }

    private MagicLanguageBlueprint(MagicLanguageBlueprint? priority, MagicLanguageBlueprint? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicLanguageBlueprint ICanClone<MagicLanguageBlueprint>.CloneUnder(MagicLanguageBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// Custom, named settings for classes, values etc. as you need them in your code.
    /// For things such as `ul` or `li` or `a` tags.
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBlueprintPart>))]
    public Dictionary<string, MagicBlueprintPart> Parts { get; init; } = new();


    internal static Defaults<MagicLanguageBlueprint> DesignDefaults = new()
    {
        Fallback = new()
        {
            Parts = new()
            {
                { "li", new() { IsActive = new() { On = "active" } } }
            },
        },
        Foundation = new()
    };
}