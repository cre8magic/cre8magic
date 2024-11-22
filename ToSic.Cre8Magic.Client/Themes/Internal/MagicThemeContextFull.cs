using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Themes.Internal;

public record MagicThemeContextFull(
    string SettingsName,
    PageState PageState,
    MagicThemeSettings ThemeSettings,
    MagicThemeDesignSettings ThemeDesignSettings,
    TokenEngine PageTokens,
    Journal Journal
): MagicThemeContext(SettingsName, ThemeSettings, Journal);