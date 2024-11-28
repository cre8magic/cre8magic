using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbNodeFactory(WorkContext<MagicBreadcrumbSettings, IMagicPageDesigner> workContext)
    : MagicPagesFactoryBase(workContext)
{
    [field: AllowNull, MaybeNull]
    public MagicBreadcrumbSettings SettingsTyped => field ??= workContext.Settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner PageDesigner() => workContext.Designer ?? new MagicBreadcrumbDesigner(workContext, SettingsTyped);
}