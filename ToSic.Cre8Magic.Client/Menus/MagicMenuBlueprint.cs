using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Menu Design Settings
/// </summary>
public record MagicMenuBlueprint : MagicBlueprints, ICanClone<MagicMenuBlueprint>
{
    [PrivateApi]
    public MagicMenuBlueprint() { }

    private MagicMenuBlueprint(MagicMenuBlueprint? priority, MagicMenuBlueprint? fallback = default)
        : base(priority, fallback)
    { }

    MagicMenuBlueprint ICanClone<MagicMenuBlueprint>.CloneUnder(MagicMenuBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

}