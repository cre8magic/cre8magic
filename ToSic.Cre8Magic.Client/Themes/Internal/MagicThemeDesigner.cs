using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

/// <summary>
/// Special helper to figure out what classes should be applied to the page. 
/// </summary>
public class MagicThemeDesigner(MagicThemeContextFull context) : MagicDesignerBase(context)
{
    internal string? BodyClasses(ITokenReplace tokens, string? additionalClasses)
    {
        var themeDesign = Context.ThemeDesignSettings;

        if (themeDesign == null) throw new ArgumentException("Can't continue without CSS specs", nameof(themeDesign));

        // Make a copy...
        var classes = themeDesign.MagicContext.ToList();
        classes.Add(themeDesign.PageIsHome?.Get(context.PageState.Page.Path == ""));
        if (additionalClasses.HasText())
            classes.Add(additionalClasses);

        // Do these once multi-language is better
        //1.5 Set the page-root-neutral-### class
        // do once Multilanguage is good


        //4.1 Set lang- class
        // do once lang is clear
        //4.2 Set the lang-root- class
        // do once lang is clear
        //4.3 Set the lang-neutral- class
        // do once lang is clear

        var bodyClasses = string.Join(" ", classes.Where(c => c.HasValue())).Replace("  ", " ");

        return tokens.Parse(bodyClasses);
    }


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