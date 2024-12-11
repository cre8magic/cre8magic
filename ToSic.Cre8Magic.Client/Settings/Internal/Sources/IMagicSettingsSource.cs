using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Settings.Internal.Sources;

public interface IMagicSettingsSource
{
    List<DataWithJournal<MagicSpellsBook>> SpellsBooks(MagicThemePackage themePackage);

    /// <summary>
    /// Priority, high number means higher priority
    /// </summary>
    int Priority { get; }
}