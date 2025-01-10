using ToSic.Cre8magic.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal.Sources;

public interface IMagicSpellsBooksSource
{
    List<DataWithJournal<MagicSpellsBook>> SpellsBooks(MagicThemePackage themePackage);

    /// <summary>
    /// Priority, high number means higher priority
    /// </summary>
    int Priority { get; }
}