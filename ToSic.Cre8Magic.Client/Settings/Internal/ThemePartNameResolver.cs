using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Internal;

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
/// <param name="mainName"></param>
/// <param name="themeSettingsParts"></param>
internal class ThemePartNameResolver(string mainName, NamedSettings<MagicThemePartSettings> themeSettingsParts)
{
    internal ThemePartNameResolver(MagicThemeContext themeCtx)
        : this(themeCtx.SettingsName, themeCtx.ThemeSettings.Parts)
    { }

    public (string BestName, List<string> Messages) GetMostRelevantSettingsName(string? possibleName, string? prefixToCheck)
    {
        var (initialName, journal) = GetBestSettingsName(possibleName, mainName);

        // Check if we have a name-remap to consider
        // If the first test fails, we try again with the prefix
        var betterName = themeSettingsParts.GetPartRenameOrNull(initialName);

        // If the better name wants to use the main config name ("=") then use that and exit
        if (betterName == MagicConstants.InheritName)
            return(mainName, journal.Concat([$"switched to inherit '{mainName}'"]).ToList());

        if (betterName == null && !string.IsNullOrEmpty(prefixToCheck) && !initialName.StartsWith(prefixToCheck))
            betterName = themeSettingsParts.GetPartRenameOrNull($"{prefixToCheck}{initialName}");

        if (!betterName.HasValue())
            return (initialName, journal);

        var messages = new List<string>(journal) { $"updated config to '{initialName}'" };
        return (betterName, messages);

    }


    /// <summary>
    /// Figure out which settings-name can be used (not empty, using "=", etc.).
    /// It will first try the preferred option, then the fallback, and if that doesn't exist, it will use the default name.
    /// </summary>
    /// <param name="preferred"></param>
    /// <param name="mainName"></param>
    /// <returns></returns>
    public static (string BestName, List<string> Journal) GetBestSettingsName(string? preferred, string mainName)
    {
        var journal = new List<string> { $"Initial Settings Name: '{preferred}'" };

        // Check if it's just a "=" symbol, which means "inherit"
        if (preferred == MagicConstants.InheritName)
        {
            preferred = mainName;
            journal.Add($"switched to inherit '{mainName}'");
        }

        // If we have a value, use that
        if (preferred.HasText())
            return (preferred, journal);

        // If we don't have a preferred name, then don't use the main name (could be "Sidebar" or something) but instead use Default
        journal.Add($"Settings Name changed to '{MagicConstants.Default}'");
        return (MagicConstants.Default, journal);
    }

}