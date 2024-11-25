using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal.Sources;

public interface IMagicSettingsSource
{
    List<DataWithJournal<MagicSettingsCatalog>> Catalogs(MagicPackageSettings packageSettings);

    /// <summary>
    /// Priority, high number means higher priority
    /// </summary>
    int Priority { get; }
}