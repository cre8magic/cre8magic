using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerBlueprint: SettingsWithInherit, ICanClone<MagicContainerBlueprint>
{
    [PrivateApi]
    public MagicContainerBlueprint() { }

    private MagicContainerBlueprint(MagicContainerBlueprint? priority, MagicContainerBlueprint? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicContainerBlueprint ICanClone<MagicContainerBlueprint>.CloneUnder(MagicContainerBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettingsPart>))]
    public Dictionary<string, MagicDesignSettingsPart> Parts { get; init; } = new(StringComparer.InvariantCultureIgnoreCase);


    internal static Defaults<MagicContainerBlueprint> Defaults = new(new()
    {
        Parts = new()
    });

}