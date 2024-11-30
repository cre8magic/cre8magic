using System.Text.Json.Serialization;
using Oqtane.Models;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettings: MagicSettingsBase, ICanClone<MagicContainerSettings>
{
    [PrivateApi]
    public MagicContainerSettings() { }

    private MagicContainerSettings(MagicContainerSettings? priority, MagicContainerSettings? fallback = default)
        : base(priority, fallback)
    {
        DesignSettings = priority?.DesignSettings ?? fallback?.DesignSettings;
        ModuleState = priority?.ModuleState ?? fallback?.ModuleState;
    }

    MagicContainerSettings ICanClone<MagicContainerSettings>.CloneUnder(MagicContainerSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    [JsonIgnore]
    public MagicContainerBlueprint? DesignSettings { get; init; }

    public Module? ModuleState { get; init; }

    internal static Defaults<MagicContainerSettings> Defaults = new(new());

}