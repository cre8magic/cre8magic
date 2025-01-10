using ToSic.Cre8magic.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Test Book-Source to use by the settings reader
/// </summary>
internal record HasMagicLibrary(List<DataWithJournal<MagicBook>> Library): IHasMagicLibrary;