using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbNodeFactory(WorkContext<MagicBreadcrumbSettings, IMagicPageTailor> workContext)
    : MagicPagesFactoryBase(workContext)
{
    protected override IMagicPageTailor PageTailor() =>
        workContext.Tailor ?? new MagicBreadcrumbTailor(workContext, workContext.Settings);
}