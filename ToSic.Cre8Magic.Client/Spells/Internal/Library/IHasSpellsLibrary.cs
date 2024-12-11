using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal;

internal interface IHasSpellsLibrary
{
    public List<DataWithJournal<MagicSpellsBook>> Library { get; }
}