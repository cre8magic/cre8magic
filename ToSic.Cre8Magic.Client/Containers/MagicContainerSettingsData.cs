using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettingsData: SettingsWithInherit, ICanClone<MagicContainerSettingsData>
{
    public MagicContainerSettingsData() { }

    [PrivateApi]
    public MagicContainerSettingsData(MagicContainerSettingsData? priority, MagicContainerSettingsData? fallback = default)
        : base(priority, fallback)
    {
    }

    [PrivateApi]
    public MagicContainerSettingsData CloneUnder(MagicContainerSettingsData? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    internal static Defaults<MagicContainerSettingsData> Defaults = new(new());

}