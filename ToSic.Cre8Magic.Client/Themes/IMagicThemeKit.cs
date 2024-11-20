using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic.Themes;

public interface IMagicThemeKit
{
    MagicThemeSettings Settings { get; }
    MagicThemeDesigner Designer { get; }

    /// <summary>
    /// Determine if we should show a specific part
    /// </summary>
    bool ShowPart(string name);
}