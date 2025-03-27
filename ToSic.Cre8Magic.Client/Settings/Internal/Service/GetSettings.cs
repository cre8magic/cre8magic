using System.Diagnostics.CodeAnalysis;
using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Debug.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Settings.Internal;

internal class GetSettings(IMagicSettingsService settingsSvc, PageState? pageStateForCachingOnly, string? name)
{
    /// <summary>
    /// Get the Theme Context - important for checking part names
    /// </summary>
    [field: AllowNull, MaybeNull]
    private CmThemeContext ThemeCtx => field ??= settingsSvc.GetThemeContext(pageStateForCachingOnly);

    /// <summary>
    /// Find Part which contains information for these settings,
    /// e.g. what to show
    /// </summary>
    internal MagicThemePartSettings? Part => field ??= ThemeCtx.ThemeSettings.GetStable().Parts.GetValueOrDefault(name ?? "dummy-prevent-error");

    /// <summary>
    /// Special helper to get settings and design settings.
    /// 
    /// Call it by providing the settings from code (can be null), and the list of settings to get from.
    /// </summary>
    /// <typeparam name="TSettings">Settings type - the one used in code, with more properties than just the settings data.</typeparam>
    /// <param name="settings"></param>
    /// <param name="reader"></param>
    /// <param name="section">what kind of settings we're retrieving - for the name-lookup in the parts definition</param>
    /// <returns></returns>
    internal DataWithJournal<TSettings>
        GetBest<TSettings>(TSettings? settings, SettingsReader<TSettings> reader, ThemePartSectionEnum section = ThemePartSectionEnum.Settings)
        where TSettings : class, new()
    {
        // Activate this for debugging
        //if (settings is IDebugSettings { DebugThis: true } tempForDebug)
        //    tempForDebug = tempForDebug;

        if (settings is IDebugSettings { Book: not null } withBook)
            reader = reader.MaybeUseCustomBook(withBook.Book);

        // Get Settings from specified reader using the provided settings as priority to merge
        // Note that the returned data will be of the base type, not the main settings type
        var findSettings = new FindSettingsNameSpecs(ThemeCtx, name, section);
        var dataWithJournal = reader.FindAndMerge(findSettings, settings);
        return dataWithJournal;
    }
}