using ToSic.Cre8magic.Settings.Values.Internal;
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

    // note: currently does special things for the ToTop, may need a standalone Tailor for this...
    public override string? Id(string name)
    {
        var result = base.Id(name);
        return result == null && name == MagicToTopConstants.LinkToTopNameId
            ? MagicToTopConstants.LinkToTopHtmlId
            : result;
    }

    public override string? Value(string target)
    {
        var result = base.Value(target);
        return result == null && target == MagicToTopConstants.LinkToTopImage
            ? MagicToTopConstants.LinkToTopDefaultSvg
            : result;
    }
}