using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Internal;

namespace ToSic.Cre8magic.Themes.Internal;

/// <summary>
/// Helper to figure out what name we should use for the settings we retrieve.
/// Reason is that a Theme can have many parts - such as the "menu", "sidebar", etc.
/// and these in turn can have many different variations of settings.
///
/// This helper will
/// 1. First resolve any keys such as "=" to the main name
/// 2. If no name was provided, or it is blank, will use "default"
/// 3. Then check the parts list for further renames
/// </summary>
/// <param name="themeName"></param>
/// <param name="themeSettingsParts"></param>
internal class ThemePartNameResolver(string themeName, Dictionary<string, MagicThemePartSettings> themeSettingsParts)
{
    /// <summary>
    /// Generic method to check for names, since it could be run on the Settings property or on the DesignSettings property
    /// </summary>
    internal DataWithJournal<string> FindBestNameAccordingToParts(FindSettingsNameSpecs nameSpecs)
    {
        var (initialName, journal) = PickBestSettingsName(nameSpecs.Name, themeName);

        // Check if we have a name-remap to consider
        // If the first test fails, we try again with the prefix
        var betterName = themeSettingsParts.TryGetValue(initialName, out var part)
            ? part.GetSettingName(nameSpecs.Section)
            : null;

        return betterName.HasValue()
            ? new(betterName, journal.With($"updated config to '{initialName}'"))
            : new(initialName, journal);
    }

    /// <summary>
    /// Figure out which settings-name can be used (not empty, using "=", etc.).
    /// It will first try the preferred option, then the fallback, and if that doesn't exist, it will use the default name.
    /// </summary>
    /// <param name="preferred"></param>
    /// <param name="inheritName"></param>
    /// <returns></returns>
    private static DataWithJournal<string> PickBestSettingsName(string? preferred, string inheritName)
    {
        var journal = new List<string> { $"Initial Settings Name: '{preferred}'" };

        preferred = preferred?.Trim();

        // If we have a value, use that
        if (preferred.HasText())
            return new(preferred, new(journal, []));

        // We don't have a name yet. Use "Default"
        journal.Add($"Settings Name changed to '{MagicConstants.Default}'");
        return new(MagicConstants.Default, new(journal, []));
    }

}