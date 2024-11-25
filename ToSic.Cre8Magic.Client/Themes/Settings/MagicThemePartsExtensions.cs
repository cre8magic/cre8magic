namespace ToSic.Cre8magic.Themes.Settings;

internal static class MagicThemePartsExtensions
{
    /// <summary>
    /// Determine the settings name of a specific part.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static string? GetPartSettingsName(this IDictionary<string, MagicThemePartSettings> dic, string name) =>
        dic.TryGetValue(name, out var value)
            ? value.Settings
            : null;

    public static string GetPartSettingsNameOrFallback(this Dictionary<string, MagicThemePartSettings> dic, string name, string fallback) =>
        dic.GetPartSettingsName(name) ?? fallback;

    public static bool Show(this MagicThemeSettings themeSettings, string name) =>
        themeSettings.Parts.TryGetValue(name, out var value) && value.Show == true;

}