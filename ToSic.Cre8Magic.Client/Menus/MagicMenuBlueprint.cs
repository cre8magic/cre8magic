using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Menu Design Blueprint
/// </summary>
public record MagicMenuBlueprint : MagicBlueprint, ICanClone<MagicMenuBlueprint>
{
    [PrivateApi]
    public MagicMenuBlueprint() { }

    private MagicMenuBlueprint(MagicMenuBlueprint? priority, MagicMenuBlueprint? fallback = default)
        : base(priority, fallback)
    { }

    MagicMenuBlueprint ICanClone<MagicMenuBlueprint>.CloneUnder(MagicMenuBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #region Stabilized

    [PrivateApi]
    public new Stabilized GetStable() => (_stabilized ??= new(new(this))).Value;
    private IgnoreEquals<Stabilized>? _stabilized;

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public new record Stabilized(MagicMenuBlueprint MenuBlueprint) : MagicBlueprint.Stabilized(MenuBlueprint);

    #endregion


}