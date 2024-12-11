using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal.Debug;

/// <summary>
/// Test Spells-Book-Source to use by the settings reader
/// </summary>
internal record HasSpellsBook(List<DataWithJournal<MagicSpellsBook>> Books): IHasSpellsBook;