using ToSic.Cre8magic.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal;

internal interface IHasMagicLibrary
{
    public List<DataWithJournal<MagicBook>> Library { get; }
}