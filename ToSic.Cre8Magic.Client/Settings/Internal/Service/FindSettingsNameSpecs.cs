using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Settings.Internal;

internal record FindSettingsNameSpecs(
    CmThemeContext ThemeContext,
    string? Name,
    ThemePartSectionEnum Section
    );