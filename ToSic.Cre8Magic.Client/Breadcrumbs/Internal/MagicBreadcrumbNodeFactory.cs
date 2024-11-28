using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbNodeFactory(ContextWip<MagicBreadcrumbSettings, IMagicPageDesigner> context)
    : MagicPagesFactoryBase(context)
{
    [field: AllowNull, MaybeNull]
    public MagicBreadcrumbSettings SettingsTyped => field ??= context.Settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => context.Designer ?? new MagicBreadcrumbDesigner(context, SettingsTyped);
}