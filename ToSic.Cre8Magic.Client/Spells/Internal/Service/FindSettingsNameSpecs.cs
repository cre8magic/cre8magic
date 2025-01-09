using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Spells.Internal;

internal record FindSettingsNameSpecs(
    CmThemeContext ThemeContext,
    string? Name,
    ThemePartSectionEnum Section
    );