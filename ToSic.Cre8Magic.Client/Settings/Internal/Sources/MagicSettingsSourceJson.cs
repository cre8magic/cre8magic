using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Json;

namespace ToSic.Cre8magic.Settings.Internal.Sources;

/// <summary>
/// Helper to load all the magic settings which could be used by the <see cref="IMagicSettingsService"/>.
///
/// It requires that there are <see cref="MagicPackageSettings"/> which were usually configured in the theme,
/// and then passed to the SettingsService on Setup.
/// </summary>
public class MagicSettingsSourceJson(MagicSettingsCatalogLoaderJson catalogLoaderJson) : IMagicSettingsSource
{
    public int Priority => 100;

    public List<DataWithJournal<MagicSettingsCatalog>> Catalogs(MagicPackageSettings packageSettings)
    {
        if (packageSettings == null)
            throw new ArgumentNullException(nameof(packageSettings));

        if (_cache.TryGetValue(packageSettings, out var cached))
            return cached;

        if (string.IsNullOrWhiteSpace(packageSettings.SettingsJsonFile))
            return [];

        var catalogFromJson = catalogLoaderJson.LoadJson(packageSettings);

        var bundle = new List<DataWithJournal<MagicSettingsCatalog>> { catalogFromJson };
        _cache[packageSettings] = bundle;
        return bundle;
    }

    /// <summary>
    /// Note: don't make static, otherwise we can't see json-file changes
    /// </summary>
    private readonly Dictionary<MagicPackageSettings, List<DataWithJournal<MagicSettingsCatalog>>> _cache = new();
}