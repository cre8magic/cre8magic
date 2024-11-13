using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Themes.Settings;

/// <summary>
/// Special helper to figure out what classes should be applied to the page. 
/// </summary>
internal class ThemeDesigner(MagicAllSettings allSettings) : MagicDesignerBase(allSettings)
{
    internal string? BodyClasses(ITokenReplace tokens)
    {
        var css = AllSettings?.ThemeDesign;

        if (css == null) throw new ArgumentException("Can't continue without CSS specs", nameof(css));

        // Make a copy...
        var classes = css.MagicContext.ToList();
        classes.Add(css.PageIsHome?.Get(AllSettings.PageState.Page.Path == ""));

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
        if (AllSettings == null) return true;
        var pageState = AllSettings.PageState;
        var paneHasModules = pageState.Modules.Any(
            module => !module.IsDeleted
                      && module.PageId == pageState.Page.PageId
                      && module.Pane == paneName);

        return !paneHasModules;
    }

    public string? PaneClasses(string paneName) => AllSettings?.ThemeDesign.PaneIsEmpty.Get(PaneIsEmpty(paneName));

    protected override DesignSetting? GetSettings(string name) => AllSettings?.ThemeDesign.Custom.GetInvariant(name);
}