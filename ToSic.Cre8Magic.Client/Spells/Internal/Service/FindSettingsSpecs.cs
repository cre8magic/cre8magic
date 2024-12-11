using ToSic.Cre8magic.Spells.Internal.Docs;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Spells.Internal;

internal record FindSettingsSpecs(
    CmThemeContext Context,
    string? SettingsName,
    string? PartName,
    string? ThemeName,
    ThemePartSectionEnum Section,
    string? Prefix)
{
    public FindSettingsSpecs(CmThemeContext context, ISettingsForCodeUse? settings, ThemePartSectionEnum section, string? prefix)
        : this(
        context,
        section == ThemePartSectionEnum.Design ? settings?.DesignName : settings?.SettingsName, // settings?.SettingsName,
        settings?.PartName,
        context.SettingsName,
        section,
        prefix)
    { }
}