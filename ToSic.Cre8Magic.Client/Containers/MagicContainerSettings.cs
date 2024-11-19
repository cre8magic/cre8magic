using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettings: SettingsWithInherit, ICanClone<MagicContainerSettings>
{
    public MagicContainerSettings()
    { }

    public MagicContainerSettings(MagicContainerSettings? priority, MagicContainerSettings? fallback = default)
        : base(priority, fallback)
    {
        Custom = fallback?.Custom.CloneWith(priority?.Custom) ?? priority?.Custom ?? new();
    }
    public MagicContainerSettings CloneWith(MagicContainerSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// TODO: PROBABLY CHANGE TO DATA / whatever ?
    /// </summary>
    public NamedSettings<MagicDesignSettings> Custom { get; init; } = new();

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