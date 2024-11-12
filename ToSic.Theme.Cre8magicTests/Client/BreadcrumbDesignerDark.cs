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

}