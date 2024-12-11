using System.Text.Json.Serialization;
using Oqtane.Models;
using ToSic.Cre8magic.Spells;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSpell: MagicSpellBase, ICanClone<MagicContainerSpell>, IWith<Module>
{
    [PrivateApi]
    public MagicContainerSpell() { }

    private MagicContainerSpell(MagicContainerSpell? priority, MagicContainerSpell? fallback = default)
        : base(priority, fallback)
    {
        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
        ModuleState = priority?.ModuleState ?? fallback?.ModuleState;
    }

    MagicContainerSpell ICanClone<MagicContainerSpell>.CloneUnder(MagicContainerSpell? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    [JsonIgnore]
    public MagicContainerBlueprint? Blueprint { get; init; }

    public Module? ModuleState { get; init; }

    internal static Defaults<MagicContainerSpell> Defaults = new(new());

    Module? IWith<Module>.WithData { get => ModuleState; init => ModuleState = value; }
}