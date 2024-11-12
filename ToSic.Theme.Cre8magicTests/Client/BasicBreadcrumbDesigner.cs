using System.Collections.Generic;
using ToSic.Cre8magic.Pages;

namespace ToSic.Theme.Cre8magicTests.Client;

internal class BasicBreadcrumbDesigner : IMagicPageDesigner
{
    public string Classes(string tag, IMagicPage item)
    {
        // List to store CSS class names
        var classes = new List<string>();

        // Additional classes based on the HTML tag
        switch (tag.ToLower())
        {
            case "ol":
                // Use 'breadcrumb' class from Bootstrap
                classes.Add("breadcrumb");
                break;

            case "li":
                // Use 'breadcrumb-item' class from Bootstrap
                classes.Add("breadcrumb-item");
                if (item.IsCurrent) classes.Add("active");
                break;

            default:
                // Handle any other tags if necessary
                break;
        }

        // Return the CSS classes as a space-separated string
        return string.Join(" ", classes);
    }

    public string Value(string key, IMagicPage item) 
        => key.ToLower() switch
        {
            "aria-current" => item.IsCurrent ? "page" : "",
            _ => ""
        };
}