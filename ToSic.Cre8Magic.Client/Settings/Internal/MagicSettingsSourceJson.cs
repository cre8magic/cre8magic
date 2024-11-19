using ToSic.Cre8magic.Settings.Json;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="IMagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicPackageSettings"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
public class MagicSettingsSourceJson(MagicSettingsJsonService jsonService) : IMagicSettingsSource
{
    public MagicSettingsCatalog? Get(MagicPackageSettings packageSettings)
    {
        if (string.IsNullOrWhiteSpace(packageSettings.SettingsJsonFile))
            return null;

        var catalogFromJson = jsonService.LoadJson(packageSettings);

        return catalogFromJson;
    }

    public List<Exception> Exceptions => jsonService.Exceptions;
}