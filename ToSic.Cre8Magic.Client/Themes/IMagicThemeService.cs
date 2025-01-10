using Oqtane.UI;

namespace ToSic.Cre8magic.Themes;

public interface IMagicThemeService
{
    /// <summary>
    /// Get a theme kit for the current page, to customize the theme.
    /// </summary>
    /// <param name="pageState">The Page State</param>
    /// <param name="settings">The Settings; optional</param>
    /// <returns></returns>
    IMagicThemeKit ThemeKit(PageState pageState, MagicThemeSettings? settings);
}