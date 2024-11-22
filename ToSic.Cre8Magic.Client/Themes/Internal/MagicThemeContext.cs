using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;

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
    Journal Journal
)
{
    internal ThemePartNameResolver NameResolver => _nameResolver ??= new(this);
    private ThemePartNameResolver? _nameResolver;
}