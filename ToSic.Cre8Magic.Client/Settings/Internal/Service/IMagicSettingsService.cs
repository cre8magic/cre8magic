using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Settings.Internal;

public interface IMagicSettingsService
{
    /// <summary>
    /// Set up the settings service with the package settings, layout name and body classes.
    /// This will result in other controls and services being able to use these settings.
    /// Otherwise, the settings are just defaulted to some standard values.
    /// </summary>
    /// <param name="themePackage"></param>
    /// <returns></returns>
    IMagicSettingsService Setup(MagicThemePackage themePackage);

    /// <summary>
    /// Get lightweight theme context - basically the final name, settings and journal.
    /// </summary>
    /// <param name="pageStateForCachingOnly"></param>
    /// <returns></returns>
    internal CmThemeContext GetThemeContext(PageState? pageStateForCachingOnly);

    internal CmThemeContextFull GetThemeContextFull(PageState pageState);

    // TODO: MAKE INTERNAL again - temporarily public soi
    public MagicDebugSettings Debug { get; }

    internal List<DataWithJournal<MagicBook>> Library { get; }


    /// <summary>
    /// WIP: PageState for this service
    /// </summary>
    PageState? PageState { get; }

    MagicThemePackage ThemePackage { get; }

    internal TokenEngine PageTokenEngine(PageState pageState);

    IMagicSettingsService UsePageState(PageState pageState);
    MagicDebugSettings.Stabilized GetDebug(PageState pageState);

    /// <summary>
    /// Get a reader from a section of the book.
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    /// <returns></returns>
    internal SettingsReader<TSettings> GetReader<TSettings>()
        where TSettings : class, new();
}