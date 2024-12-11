using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal;

internal interface IHasSpellsBook
{
    public List<DataWithJournal<MagicSpellsBook>> Books { get; }
}