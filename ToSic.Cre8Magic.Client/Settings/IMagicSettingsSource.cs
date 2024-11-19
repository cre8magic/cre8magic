using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

public interface IMagicSettingsSource: IHasSystemMessages
{
    MagicSettingsCatalog? Get(MagicPackageSettings packageSettings);
}