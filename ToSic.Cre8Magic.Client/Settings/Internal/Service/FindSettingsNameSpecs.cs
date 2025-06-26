using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Settings.Internal;

internal record FindSettingsNameSpecs(
    CmThemeContext ThemeContext,
    string? Name,

    // Section type, for renaming / redirects
    // Not sure if this is still a thing...
    // Since the concept of names has been simplified
    ThemePartSectionEnum Section
    );