using Oqtane.UI;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Themes.Internal;

public record MagicThemeContextFull(
    string SettingsName,
    PageState PageState,
    MagicThemeSettings ThemeSettings,
    MagicThemeDesignSettings ThemeDesignSettings,
    TokenEngine PageTokens,
    List<string> Journal
): MagicThemeContext(SettingsName, ThemeSettings, Journal);