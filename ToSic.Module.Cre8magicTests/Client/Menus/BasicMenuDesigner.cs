using System.Collections.Generic;
using ToSic.Cre8magic.Pages;

namespace ToSic.Module.Cre8MagicTests.Client.Menus;

internal class BasicMenuDesigner : IMagicPageDesigner
{
    public string Classes(string tag, IMagicPage page)
    {
        // List to store CSS class names
        var classes = new List<string>();

        // Base class for the current level
        var levelClass = $"level{page.MenuLevel}";
        classes.Add(levelClass);

        // Additional classes based on the HTML tag
        switch (tag.ToLower())
        {
            case "ul":
                // Use 'nav' class from Bootstrap
                classes.Add("nav");
                // For nested menus, add 'flex-column' to stack items vertically
                if (page.MenuLevel > 1) classes.Add("flex-column");
                break;

            case "li":
                // Use 'nav-item' class from Bootstrap
                classes.Add("nav-item");
                break;

            case "a":
                // Use 'nav-link' class from Bootstrap
                classes.Add("nav-link");
                // If the page is active, add 'active' class
                if (page.IsActive) classes.Add("active");
                break;

            case "span":
                // Add any specific classes for 'span' elements if needed
                break;

            default:
                // Handle any other tags if necessary
                break;
        }

        // Return the CSS classes as a space-separated string
        return string.Join(" ", classes);
    }




    public string Value(string key, IMagicPage page)
    {
        return "yyy";
    }
}