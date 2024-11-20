namespace ToSic.Cre8magic.Themes.Internal;

/// <summary>
/// Lightweight context, mainly used for retrieving settings parts.
/// </summary>
/// <param name="SettingsName"></param>
/// <param name="ThemeSettings"></param>
/// <param name="Journal"></param>
public record MagicThemeContext(
    string SettingsName,
    MagicThemeSettings ThemeSettings,
    List<string> Journal
);