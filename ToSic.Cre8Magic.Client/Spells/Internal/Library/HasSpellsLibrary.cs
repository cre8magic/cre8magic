using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Spells.Internal;

/// <summary>
/// Test Spells-Book-Source to use by the settings reader
/// </summary>
internal record HasSpellsLibrary(List<DataWithJournal<MagicSpellsBook>> Library): IHasSpellsLibrary;