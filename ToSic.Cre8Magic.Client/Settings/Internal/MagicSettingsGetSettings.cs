using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Docs;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Settings.Internal;

internal static class MagicSettingsGetSettings
{
    internal static Data3WithJournal<TSettingsBase, CmThemeContext, MagicThemePartSettings?> GetBestSettings<TSettings, TSettingsBase>(
    this IMagicSpellsService spellsSvc,
    PageState? pageStateForCachingOnly,
    TSettings? settings,
    SettingsReader<TSettingsBase> settingsReader,
    string settingsPrefix,
    string defaultPartNameForShow
)
    where TSettings : TSettingsBase, ISettingsForCodeUse, new()
    where TSettingsBase : class, new()
    {
        // Get the Theme Context - important for checking part names
        var themeCtx = spellsSvc.GetThemeContext(pageStateForCachingOnly);

        // Activate this for debugging
        //if (settings is IDebugSettings { DebugThis: true } tempForDebug)
        //    tempForDebug = tempForDebug;

        // Find Part which contains information for these settings,
        // e.g. what to show
        var parts = themeCtx.ThemeSpell.Parts;
        var part = parts.GetValueOrDefault(settings?.PartName ?? "dummy-prevent-error")
            ?? parts.GetValueOrDefault(defaultPartNameForShow);

        // Get Settings from specified reader using the provided settings as priority to merge
        // Note that the returned data will be of the base type, not the main settings type
        var findSettings = new FindSettingsSpecs(themeCtx, settings, ThemePartSectionEnum.Settings, settingsPrefix);
        if (settings is IDebugSettings { Book: not null } withBook)
            settingsReader = settingsReader.MaybeUseCustomSpellsBook(withBook.Book);
        var (mergedSettings, journal) = settingsReader.FindAndMerge(findSettings, settings, skipCache: true);

        return new(mergedSettings, themeCtx, part, journal);
    }

    /// <summary>
    /// Special helper to get a very common pair of settings and design settings.
    /// 
    /// Call it by providing the settings from code (can be null), and the list of settings to get from.
    /// Same for DesignSettings from code (can be null) and the list of settings to get from.
    /// 
    /// Because the return type is the main type, and the stored settings are always the pure data-type,
    /// it needs a factory to create the final object / recombine the new design settings in to the final settings.
    /// </summary>
    /// <typeparam name="TSettings">Settings type - the one used in code, with more properties than just the settings data.</typeparam>
    /// <typeparam name="TSettingsBase">The base type of the settings, just containing the data</typeparam>
    /// <typeparam name="TDesign">The type of the design settings.</typeparam>
    /// <param name="spellsSvc"></param>
    /// <param name="pageState"></param>
    /// <param name="settings"></param>
    /// <param name="settingsReader"></param>
    /// <param name="dSettings"></param>
    /// <param name="designReader"></param>
    /// <param name="settingPrefix"></param>
    /// <param name="defaultPartNameForShow"></param>
    /// <param name="finalize"></param>
    /// <returns></returns>
    internal static Data3WithJournal<TSettings, CmThemeContext, MagicThemePartSettings?> GetBestSettingsAndDesignSettings<TSettings, TSettingsBase, TDesign>(
        this IMagicSpellsService spellsSvc,
        PageState pageState,
        TSettings? settings,
        SettingsReader<TSettingsBase> settingsReader,
        TDesign? dSettings,
        SettingsReader<TDesign> designReader,
        string settingPrefix,
        string defaultPartNameForShow,
        Func<TSettingsBase, TDesign, TSettings> finalize
    )
        where TSettings : TSettingsBase, ISettingsForCodeUse, new()
        where TDesign : class, new() where TSettingsBase : class, new()
    {

        var (mergedSettings, themeCtx, part, journal) = spellsSvc.GetBestSettings(pageState, settings, settingsReader, settingPrefix, defaultPartNameForShow);

        // Activate this for debugging
        //if (settings is IDebugSettings { DebugThis: true } tempForDebug)
        //    tempForDebug = tempForDebug;

        // Get Design Settings from specified reader using the provided design settings as priority to merge
        var findSettings = new FindSettingsSpecs(themeCtx, settings, ThemePartSectionEnum.Design, settingPrefix);
        if (dSettings is IDebugSettings { Book: not null } withBlueprintsBook)
            designReader = designReader.MaybeUseCustomSpellsBook(withBlueprintsBook.Book);
        var (designSettings, designJournal) = designReader.FindAndMerge(findSettings, dSettings);

        // Reconstruct the expected settings type to merge in the design use the provided finalize function
        var fullSettings = finalize(mergedSettings, designSettings);

        return new(fullSettings, themeCtx, part, journal.With(designJournal));
    }

}