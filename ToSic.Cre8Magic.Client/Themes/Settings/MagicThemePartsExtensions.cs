using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Themes.Settings;

internal static class MagicThemePartsExtensions
{
    /// <summary>
    /// Determine the configuration name of a specific part.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string? GetPartRenameOrNull(this NamedSettings<MagicThemePartSettings> dic, string name) =>
        dic.TryGetValue(name, out var value)
            ? value.Settings
            : null;

    public static string GetPartRenameOrFallback(this NamedSettings<MagicThemePartSettings> dic, string name, string fallback) =>
        dic.GetPartRenameOrNull(name) ?? fallback;

    /// <summary>
    /// Determine the name of the design configuration of a specific part
    /// </summary>
    public static string? GetPartDesignRenameOrNull(this NamedSettings<MagicThemePartSettings> dic, string name) =>
        dic.TryGetValue(name, out var value)
            ? value.Design
            : null;

    public static bool Show(this MagicThemeSettings themeSettings, string name) =>
        themeSettings.Parts.TryGetValue(name, out var value) && value.Show == true;

}