using ToSic.Cre8magic.Spells.Internal.Docs;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Spells.Internal;

internal record FindSettingsNameSpecs(
    CmThemeContext Context,
    string? SettingsName,
    string? PartName,
    string? ThemeName,
    ThemePartSectionEnum Section
    )
{
    public FindSettingsNameSpecs(CmThemeContext context, ISettingsForCodeUse? settings, ThemePartSectionEnum section)
        : this(
        context,
        section == ThemePartSectionEnum.Design ? settings?.BlueprintName : settings?.SettingsName,
        settings?.Name,
        context.Name,
        section
        )
    { }
}