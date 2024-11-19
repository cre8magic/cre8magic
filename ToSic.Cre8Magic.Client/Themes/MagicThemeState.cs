using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Themes;

/// <summary>
/// TODO: PROBABLY INTERFACE
/// </summary>
public record MagicThemeState(MagicThemeContext context, MagicThemeDesigner designer)
{
    public MagicThemeSettings Settings => context.ThemeSettings;

    public MagicThemeDesignSettings DesignSettings => context.ThemeDesignSettings;

    public MagicThemeDesigner Designer => designer;

    /// <summary>
    /// Determine if we should show a specific part
    /// </summary>
    public bool ShowPart(string name) =>
        Settings.Parts.TryGetValue(name, out var partSettings) && partSettings.Show == true;

}