using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettings: SettingsWithInherit, ICanClone<MagicContainerSettings>
{
    public MagicContainerSettings()
    { }

    public MagicContainerSettings(MagicContainerSettings? priority, MagicContainerSettings? fallback = default)
        : base(priority, fallback)
    {
        if (fallback?.Custom == null)
        {
            Custom = priority?.Custom == null ? new(StringComparer.InvariantCultureIgnoreCase) : new(priority.Custom, StringComparer.InvariantCultureIgnoreCase);
            return;
        }

        var copyFallback = new Dictionary<string, MagicDesignSettings>(fallback.Custom, StringComparer.InvariantCulture);

        MergeHelper.MergeDictionaries(copyFallback, priority?.Custom);
        Custom = copyFallback;
    }
    public MagicContainerSettings CloneUnder(MagicContainerSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// TODO: PROBABLY CHANGE TO DATA / whatever ?
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettings>))]
    public Dictionary<string, MagicDesignSettings> Custom { get; init; } = new(StringComparer.InvariantCultureIgnoreCase);

    private static readonly MagicContainerSettings FbAndF = new()
    {
        Custom = new()
    };

    internal static Defaults<MagicContainerSettings> Defaults = new()
    {
        Fallback = FbAndF,
        Foundation = FbAndF,
    };

}