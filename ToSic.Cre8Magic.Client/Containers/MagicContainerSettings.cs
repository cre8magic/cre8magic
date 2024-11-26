using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettings: SettingsWithInherit, ICanClone<MagicContainerSettings>
{
    public MagicContainerSettings() { }

    [PrivateApi]
    public MagicContainerSettings(MagicContainerSettings? priority, MagicContainerSettings? fallback = default)
        : base(priority, fallback)
    {
        TestData = priority?.TestData ?? fallback?.TestData;
        Custom = MergeHelper.CloneMergeDictionaries(priority?.Custom, fallback?.Custom);
    }

    [PrivateApi]
    public MagicContainerSettings CloneUnder(MagicContainerSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    public string? TestData { get; init; }

    /// <summary>
    /// TODO: PROBABLY CHANGE TO DATA / whatever ?
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettingsPart>))]
    public Dictionary<string, MagicDesignSettingsPart> Custom { get; init; } = new(StringComparer.InvariantCultureIgnoreCase);


    internal static Defaults<MagicContainerSettings> Defaults = new(new()
    {
        Custom = new()
    });

}