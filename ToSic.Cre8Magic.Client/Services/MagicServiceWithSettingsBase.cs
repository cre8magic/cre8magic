using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Services;

public abstract class MagicServiceWithSettingsBase
{
    public void InitSettings(MagicAllSettings allSettings) => AllSettings = allSettings;

    public MagicAllSettings? AllSettings { get; private set; }

}