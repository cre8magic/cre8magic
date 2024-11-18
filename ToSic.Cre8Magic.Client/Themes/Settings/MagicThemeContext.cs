using Oqtane.UI;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Themes.Settings;

public record MagicThemeContext(
    string SettingsName,
    PageState PageState,
    MagicThemeSettings ThemeSettings,
    MagicThemeDesignSettings ThemeDesignSettings,
    TokenEngine PageTokens,
    List<string> Journal
)
{
    /// <summary>
    /// TODO: probably move to MagicThemeSettings
    /// </summary>
    public string SettingsName { get; init; } = SettingsName;
}