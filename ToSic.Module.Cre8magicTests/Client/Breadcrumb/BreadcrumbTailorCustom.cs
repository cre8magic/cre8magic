using System.Collections.Generic;
using ToSic.Cre8magic.Pages;

namespace ToSic.Module.Cre8MagicTests.Client.Breadcrumb;

internal class BreadcrumbTailorCustom: MagicPageDesignerBasic
{
    public override string Classes(string tag, IMagicPage item)
    {
        // List to store CSS class names
        var classes = new List<string> { "custom" };

        // Additional classes based on the HTML tag
        switch (tag.ToLower())
        {
            case "ol":
                // Use 'breadcrumb' class from Bootstrap
                classes.Add("breadcrumb");
                classes.Add("bg-dark");
                classes.Add("text-white");
                break;

            case "li":
                // Use 'breadcrumb-item' class from Bootstrap
                classes.Add("breadcrumb-item");
                if (item.IsActive) classes.Add("active");
                break;

            case "a" or "span":
                classes.Add("text-white"); ;
                break;

            default:
                // Handle any other tags if necessary
                break;
        }

        // Return the CSS classes as a space-separated string
        return string.Join(" ", classes);
    }
}