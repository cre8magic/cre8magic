using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Client.Containers.Settings;

public record MagicContainerSettings: SettingsWithInherit
{
    public MagicContainerSettings()
    { }

    public MagicContainerSettings(MagicContainerSettings? priority, MagicContainerSettings? fallback = default)
        : base(priority, fallback)
    {
        // TODO!
        //Custom = new NamedSettings<DesignSetting>(priority?.Custom, fallback?.Custom);
    }

    public NamedSettings<DesignSetting> Custom { get; init; } = new();

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