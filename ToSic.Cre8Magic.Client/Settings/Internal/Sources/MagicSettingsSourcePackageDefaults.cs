using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal.Sources;

/// <summary>
/// Load the package settings defaults.
///
/// Fairly trivial, but the goal is that all sources implement the same interface.
/// </summary>
public class MagicSettingsSourcePackageDefaults : IMagicSettingsSource
{
    public int Priority => -100;

    public List<DataWithJournal<MagicSettingsCatalog>> Catalog(MagicPackageSettings packageSettings) =>
        packageSettings == null
            ? throw new ArgumentNullException(nameof(packageSettings))
            : packageSettings.Defaults == null
                ? []
                : [new(packageSettings.Defaults, new())];
}