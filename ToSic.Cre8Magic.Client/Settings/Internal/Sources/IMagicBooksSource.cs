using ToSic.Cre8magic.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal.Sources;

public interface IMagicBooksSource
{
    List<DataWithJournal<MagicBook>> Books(MagicThemePackage themePackage);

    /// <summary>
    /// Priority, high number means higher priority
    /// </summary>
    int Priority { get; }
}