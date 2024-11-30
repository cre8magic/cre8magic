using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumbs;

/// <summary>
/// Default designer for breadcrumbs in Bootstrap 5.
/// Will use the standard Bootstrap 5 classes for breadcrumbs.
/// - ol: breadcrumb
/// - li: breadcrumb-item
/// - active state: "active"
/// </summary>
public class MagicBreadcrumbTailorBs5 : MagicPageDesignerBasic
{
    public MagicBreadcrumbTailorBs5()
    {
        LookupClassActive = "active";
        LookupClasses = new Dictionary<string, string>
        {
            { "ol", "breadcrumb" },
            { "li", "breadcrumb-item" }
        };
    }
}