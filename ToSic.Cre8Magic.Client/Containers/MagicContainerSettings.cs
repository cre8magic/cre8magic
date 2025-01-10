using System.Text.Json.Serialization;
using Oqtane.Models;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettings: MagicSettings, ICanClone<MagicContainerSettings>, IWith<Module?>
{
    [PrivateApi]
    public MagicContainerSettings() { }

    private MagicContainerSettings(MagicContainerSettings? priority, MagicContainerSettings? fallback = default)
        : base(priority, fallback)
    {
        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
        ModuleState = priority?.ModuleState ?? fallback?.ModuleState;
    }

    MagicContainerSettings ICanClone<MagicContainerSettings>.CloneUnder(MagicContainerSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    [JsonIgnore]
    public MagicContainerBlueprint? Blueprint { get; init; }

    public Module? ModuleState { get; init; }

    internal static Defaults<MagicContainerSettings> Defaults = new(new());

    Module? IWith<Module?>.WithData { get => ModuleState; init => ModuleState = value; }
}