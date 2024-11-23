using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal.Debug;

/// <summary>
/// Test Catalog-Source to use by the settings reader
/// </summary>
internal record HasCatalogs(List<DataWithJournal<MagicSettingsCatalog>> Catalogs): IHasCatalogs;