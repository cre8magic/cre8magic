using Oqtane.UI;
using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Settings.Values.Internal;
using ToSic.Cre8magic.Tokens;


namespace ToSic.Cre8magic.PageContexts;

/// <summary>
/// Special helper to figure out what classes should be applied to the page. 
/// </summary>
public class MagicPageContextTailor(MagicPageContextSettings settings, PageState pageState)
{
    internal string? BodyClasses(ITokenReplace tokens, string? additionalClasses)
    {
        //var themeDesign = context.ThemeDesignSettings;

        //if (themeDesign == null) throw new ArgumentException("Can't continue without CSS specs", nameof(themeDesign));

        // Make a copy...
        var stable = settings.GetStable();
        List<string?> classes = [
            ..stable.ClassList,
            stable.PageIsHome.Get(pageState.Page.Path == ""),
            additionalClasses,
        ]!;
        //if (additionalClasses.HasText())
        //    classes.Add(additionalClasses);

        // Do these once multi-language is better
        //1.5 Set the page-root-neutral-### class
        // do once Multilanguage is good


        //4.1 Set lang- class
        // do once lang is clear
        //4.2 Set the lang-root- class
        // do once lang is clear
        //4.3 Set the lang-neutral- class
        // do once lang is clear

        var bodyClasses = string.Join(" ", classes.Where(c => c.HasText())).Replace("  ", " ");

        return tokens.Parse(bodyClasses);
    }

}