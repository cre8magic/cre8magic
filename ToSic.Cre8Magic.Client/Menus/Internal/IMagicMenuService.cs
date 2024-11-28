using Oqtane.UI;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Service to get a tree of pages for a menu.
/// </summary>
public interface IMagicMenuService
{
    public bool NoInheritSettingsWip { get; set; }

    /// <summary>
    /// Get the menu kit containing all items for the current page and specified settings.
    /// </summary>
    /// <param name="pageState"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    IMagicMenuKit MenuKit(PageState pageState, MagicMenuSettings? settings = default);
}