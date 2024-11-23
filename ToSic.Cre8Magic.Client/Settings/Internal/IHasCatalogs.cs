using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal;

internal interface IHasCatalogs
{
    public List<DataWithJournal<MagicSettingsCatalog>> Catalogs { get; }
}