using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Tailors;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerBlueprint: MagicBlueprint, ICanClone<MagicContainerBlueprint>
{
    [PrivateApi]
    public MagicContainerBlueprint() { }

    private MagicContainerBlueprint(MagicContainerBlueprint? priority, MagicContainerBlueprint? fallback = default)
        : base(priority, fallback)
    { }

    MagicContainerBlueprint ICanClone<MagicContainerBlueprint>.CloneUnder(MagicContainerBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    internal static Defaults<MagicContainerBlueprint> Defaults = new(new()
    {
        Parts = new()
    });

}