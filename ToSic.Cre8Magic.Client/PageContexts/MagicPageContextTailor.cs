using Oqtane.UI;
using ToSic.Cre8magic.Settings.Values.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts;

/// <summary>
/// Special helper to figure out what classes should be applied to the page. 
/// </summary>
public class MagicPageContextTailor(MagicPageContextSpell spell, PageState pageState)
{
    internal string? BodyClasses(ITokenReplace tokens, string? additionalClasses)
    {
        //var themeDesign = context.ThemeDesignSettings;

        //if (themeDesign == null) throw new ArgumentException("Can't continue without CSS specs", nameof(themeDesign));

        // Make a copy...
        List<string?> classes = (spell.ClassList?.ToList() ?? [])!;
        classes.Add(spell.PageIsHome?.Get(pageState.Page.Path == ""));
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

}