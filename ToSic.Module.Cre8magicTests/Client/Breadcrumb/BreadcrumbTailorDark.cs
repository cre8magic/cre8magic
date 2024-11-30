using System.Collections.Generic;
using ToSic.Cre8magic.Pages;

namespace ToSic.Module.Cre8MagicTests.Client.Breadcrumb;

internal class BreadcrumbTailorDark : MagicPageDesignerBasic
{
    public BreadcrumbTailorDark()
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