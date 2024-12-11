using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Tailors;

/// <summary>
/// Menu Design Settings
/// </summary>
public record MagicBlueprint : MagicInheritsBase, ICanClone<MagicBlueprint>
{
    [PrivateApi]
    public MagicBlueprint() { }

    internal MagicBlueprint(MagicBlueprint? priority, MagicBlueprint? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicBlueprint ICanClone<MagicBlueprint>.CloneUnder(MagicBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// Custom, named settings for classes, values etc. as you need them in your code.
    /// For things such as `ul` or `li` or `a` tags.
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBlueprintPart>))]
    public Dictionary<string, MagicBlueprintPart> Parts { get; init; } = new(StringComparer.OrdinalIgnoreCase);

}