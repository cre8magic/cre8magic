using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumb;

/// <summary>
/// Default designer for breadcrumbs in Bootstrap 5.
/// Will use the standard Bootstrap 5 classes for breadcrumbs.
/// </summary>
public class MagicBreadcrumbDesignerBs5 : MagicPageDesignerBasic
{
    public MagicBreadcrumbDesignerBs5()
    {
        LookupClassActive = "active";
        LookupClasses = new Dictionary<string, string>
        {
            { "ol", "breadcrumb" },
            { "li", "breadcrumb-item" }
        };
    }
}