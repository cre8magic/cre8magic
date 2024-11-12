using System.Collections.Generic;
using ToSic.Cre8magic.Pages;

namespace ToSic.Theme.Cre8magicTests.Client;

internal class BreadcrumbDesignerDark : MagicPageDesignerBasic
{
    public BreadcrumbDesignerDark()
    {
        LookupClassActive = "active";
        LookupClasses = new Dictionary<string, string>
        {
            { All, "custom"},
            { "ol", "breadcrumb bg-dark text-white" },
            { "li", "breadcrumb-item" },
            { "a", "text-white" },
            { "span", "text-white" }
        };
    }

    //public override string Classes(string tag, IMagicPage item)
    //{
    //    // List to store CSS class names
    //    var classes = new List<string> { "custom" };

    //    // Additional classes based on the HTML tag
    //    switch (tag.ToLower())
    //    {
    //        case "ol":
    //            // Use 'breadcrumb' class from Bootstrap
    //            classes.Add("breadcrumb");
    //            classes.Add("bg-dark");
    //            classes.Add("text-white");
    //            break;

    //        case "li":
    //            // Use 'breadcrumb-item' class from Bootstrap
    //            classes.Add("breadcrumb-item");
    //            if (item.IsCurrent) classes.Add("active");
    //            break;

    //        case "a" or "span":
    //            classes.Add("text-white"); ;
    //            break;

    //        default:
    //            // Handle any other tags if necessary
    //            break;
    //    }

    //    // Return the CSS classes as a space-separated string
    //    return string.Join(" ", classes);
    //}
}