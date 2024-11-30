using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Tailors;

namespace ToSic.Cre8magic.Themes.Internal;

/// <summary>
/// Special helper to figure out what classes should be applied to the page. 
/// </summary>
public class MagicThemeTailor(CmThemeContextFull context) : MagicTailorBase(context.PageTokens, context.ThemeBlueprint.Parts)
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
        context?.ThemeBlueprint.PaneIsEmpty.Get(PaneIsEmpty(paneName));
}