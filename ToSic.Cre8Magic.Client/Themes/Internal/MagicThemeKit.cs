using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Themes.Internal;

internal record MagicThemeKit : IMagicThemeKit
{
    internal required MagicThemeContext Context { get; init; }

    public MagicThemeSettings Settings => Context.ThemeSettings;

    public MagicThemeDesignSettings DesignSettings => Context.ThemeDesignSettings;

    public required MagicThemeDesigner Designer { get; init; }

    /// <summary>
    /// Determine if we should show a specific part
    /// </summary>
    public bool ShowPart(string name) =>
        Settings.Parts.TryGetValue(name, out var partSettings) && partSettings.Show == true;

}