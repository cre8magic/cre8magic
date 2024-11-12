using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Services;

public abstract class MagicServiceWithSettingsBase
{
    public void InitSettings(MagicAllSettings allSettings) => GlobalSettings = allSettings;

    public MagicAllSettings? GlobalSettings { get; private set; }

}