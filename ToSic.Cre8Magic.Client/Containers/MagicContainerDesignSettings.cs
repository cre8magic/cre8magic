using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerDesignSettings: SettingsWithInherit, ICanClone<MagicContainerDesignSettings>
{
    public MagicContainerDesignSettings() { }

    [PrivateApi]
    private MagicContainerDesignSettings(MagicContainerDesignSettings? priority, MagicContainerDesignSettings? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    [PrivateApi]
    MagicContainerDesignSettings ICanClone<MagicContainerDesignSettings>.CloneUnder(MagicContainerDesignSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettingsPart>))]
    public Dictionary<string, MagicDesignSettingsPart> Parts { get; init; } = new(StringComparer.InvariantCultureIgnoreCase);


    internal static Defaults<MagicContainerDesignSettings> Defaults = new(new()
    {
        Parts = new()
    });

}