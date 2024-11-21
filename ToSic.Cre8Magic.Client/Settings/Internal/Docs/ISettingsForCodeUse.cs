using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Settings.Internal.Docs;

/// <summary>
/// Internal interface to specify settings which extend the ...Data settings.
/// They will usually have things such as `PartName`.
/// </summary>
internal interface ISettingsForCodeUse
{
    /// <summary>
    /// Name to identify this part.
    /// This information is used to load settings (menu settings and design settings)
    /// </summary>
    public string? PartName { get; init; }

    /// <summary>
    /// Name to identify which settings to load.
    /// This is used before looking in the Theme Part.
    /// If not specified, will check the theme part for a name it provides, or use the theme-part name to find the settings.
    /// </summary>
    public string? SettingsName { get; init; }

    public string? DesignName { get; init; }

    internal FindSettingsSpecs CreateFindSpecs(MagicThemeContext context, ThemePartSectionEnum section, string? prefix) =>
        new(context, SettingsName, PartName, context.SettingsName, section, prefix);
}