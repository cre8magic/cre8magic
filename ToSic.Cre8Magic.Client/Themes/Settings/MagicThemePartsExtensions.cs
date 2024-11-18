using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Themes.Settings;

internal static class MagicThemePartsExtensions
{
    /// <summary>
    /// Determine the configuration name of a specific part.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string? GetThemePartRenameOrNull(this NamedSettings<MagicThemePartSettings> dic, string name) =>
        dic.TryGetValue(name, out var value)
            ? value.Configuration
            : null;

    /// <summary>
    /// Determine the name of the design configuration of a specific part
    /// </summary>
    public static string? GetThemeDesignRenameOrNull(this NamedSettings<MagicThemePartSettings> dic, string name) =>
        dic.TryGetValue(name, out var value)
            ? value.Design
            : null;

    public static string GetThemePartRenameOrFallback(this NamedSettings<MagicThemePartSettings> dic, string name, string fallback) =>
        dic.GetThemePartRenameOrNull(name) ?? fallback;

    public static bool Show(this MagicThemeSettings themeSettings, string name) =>
        themeSettings.Parts.TryGetValue(name, out var value) && value.Show == true;

}