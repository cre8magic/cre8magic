using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Services;

public abstract class MagicServiceWithSettingsBase
{
    public void InitSettings(MagicAllSettings allSettings) => GlobalSettings = allSettings;

    // TODO: RENAME TO AllSettings
    public MagicAllSettings? GlobalSettings { get; private set; }

}