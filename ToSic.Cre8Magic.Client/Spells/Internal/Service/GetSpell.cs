using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Spells.Internal.Debug;
using ToSic.Cre8magic.Spells.Internal.Docs;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Spells.Internal;

internal static class GetSpell
{
    internal static Data3WithJournal<TSettingsBase, CmThemeContext, MagicThemePartSettings?> 
        GetBestSpell<TSettings, TSettingsBase>(
            this IMagicSpellsService spellsSvc,
            PageState? pageStateForCachingOnly,
            TSettings? settings,
            SettingsReader<TSettingsBase> settingsReader)
    where TSettings : TSettingsBase, ISettingsForCodeUse, new()
    where TSettingsBase : class, new()
    {
        // Get the Theme Context - important for checking part names
        var themeCtx = spellsSvc.GetThemeContext(pageStateForCachingOnly);

        var section = ThemePartSectionEnum.Settings;
        var name = settings?.Name;

        // Activate this for debugging
        //if (settings is IDebugSettings { DebugThis: true } tempForDebug)
        //    tempForDebug = tempForDebug;

        if (settings is IDebugSettings { Book: not null } withBook)
            settingsReader = settingsReader.MaybeUseCustomSpellsBook(withBook.Book);

        // Get Settings from specified reader using the provided settings as priority to merge
        // Note that the returned data will be of the base type, not the main settings type
        var findSettings = new FindSettingsNameSpecs(themeCtx, settings?.Name, section);
        var dataWithJournal = settingsReader.FindAndMerge(findSettings, settings);


        // Find Part which contains information for these settings,
        // e.g. what to show
        var parts = themeCtx.ThemeSpell.Parts;
        var part = parts.GetValueOrDefault(settings?.Name ?? "dummy-prevent-error");

        return new(dataWithJournal.Data, themeCtx, part, dataWithJournal.Journal);
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
    /// <param name="finalize"></param>
    /// <returns></returns>
    internal static Data3WithJournal<TSettings, CmThemeContext, MagicThemePartSettings?>
        GetBestSpellAndBlueprints<TSettings, TSettingsBase, TDesign>(
            this IMagicSpellsService spellsSvc,
            PageState pageState,
            TSettings? settings,
            SettingsReader<TSettingsBase> settingsReader,
            TDesign? dSettings,
            SettingsReader<TDesign> designReader,
            Func<TSettingsBase, TDesign, TSettings> finalize)
        where TSettings : TSettingsBase, ISettingsForCodeUse, new()
        where TDesign : class, new() where TSettingsBase : class, new()
    {
        var section = ThemePartSectionEnum.Design;

        var (mergedSettings, themeCtx, part, journal) =
            GetBestSpell(spellsSvc, pageState, settings, settingsReader);

        // Activate this for debugging
        //if (settings is IDebugSettings { DebugThis: true } tempForDebug)
        //    tempForDebug = tempForDebug;

        if (dSettings is IDebugSettings { Book: not null } withBook)
            designReader = designReader.MaybeUseCustomSpellsBook(withBook.Book);

        // Get Design Settings from specified reader using the provided design settings as priority to merge
        var findSettings = new FindSettingsNameSpecs(themeCtx, settings?.Name, section);
        var dataWithJournal = designReader.FindAndMerge(findSettings, dSettings);

        // Reconstruct the expected settings type to merge in the design use the provided finalize function
        var fullSettings = finalize(mergedSettings, dataWithJournal.Data);

        return new(fullSettings, themeCtx, part, journal.With(dataWithJournal.Journal));
    }

}