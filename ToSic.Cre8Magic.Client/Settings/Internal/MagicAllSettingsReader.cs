using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Internal;

internal class MagicAllSettingsReader(MagicAllSettings allSettings)
{
    public (string BestName, List<string> Messages) GetMostRelevantSettingsName(string? originalName, string? prefixToCheck)
    {
        var messages = new List<string>();
        var (configName, journal) = GetBestSettingsName(originalName, allSettings.Name);
        messages.AddRange(journal);

        // Check if we have a name-remap to consider
        // If the first test fails, we try again with the prefix
        var menuConfig = allSettings.ThemeSettings.Parts.GetThemePartRenameOrNull(configName);
        if (menuConfig == null && !string.IsNullOrEmpty(prefixToCheck) && !configName.StartsWith(prefixToCheck))
            menuConfig = allSettings.ThemeSettings.Parts.GetThemePartRenameOrNull($"{prefixToCheck}{configName}");

        if (menuConfig.HasValue())
        {
            configName = menuConfig;
            messages.Add($"updated config to '{configName}'");
        }

        return (configName, messages);
    }


    /// <summary>
    /// Figure out which settings-name can be used (not empty, using "=", etc.).
    /// It will first try the preferred option, then the fallback, and if that doesn't exist, it will use the default name.
    /// </summary>
    /// <param name="preferred"></param>
    /// <param name="fallback"></param>
    /// <returns></returns>
    public static (string BestName, List<string> Journal) GetBestSettingsName(string? preferred, string fallback)
    {
        var debugInfo = new List<string> { $"Initial Config: '{preferred}'" };
        if (preferred.EqInvariant(MagicConstants.InheritName))
        {
            preferred = fallback;
            debugInfo.Add($"switched to inherit '{fallback}'");
        }
        if (preferred.HasText())
            return (preferred, debugInfo);

        debugInfo.Add($"Config changed to '{MagicConstants.Default}'");
        return (MagicConstants.Default, debugInfo);
    }

}