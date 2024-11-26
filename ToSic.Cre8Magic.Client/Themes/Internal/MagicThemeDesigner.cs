using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

/// <summary>
/// Special helper to figure out what classes should be applied to the page. 
/// </summary>
public class MagicThemeDesigner(CmThemeContextFull context) : MagicDesignerBase(context)
{
    private bool PaneIsEmpty(string paneName)
    {
        var pageState = context.PageState;
        var paneHasModules = pageState.Modules.Any(
            module => !module.IsDeleted
                      && module.PageId == pageState.Page.PageId
                      && module.Pane == paneName);

        return !paneHasModules;
    }

    /// <summary>
    /// Special classes for divs surrounding panes pane, especially to indicate when it's empty
    /// </summary>
    public string? PaneClasses(string paneName) =>
        Context?.ThemeDesignSettings.PaneIsEmpty.Get(PaneIsEmpty(paneName));
}