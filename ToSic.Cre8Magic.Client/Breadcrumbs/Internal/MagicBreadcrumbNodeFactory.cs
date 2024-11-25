using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbNodeFactory(ContextWip<MagicBreadcrumbSettingsWip, IMagicPageDesigner> context)
    : MagicPagesFactoryBase(context)
{
    public MagicBreadcrumbSettingsWip SettingsTyped => _settings ??= context.Settings;
    private MagicBreadcrumbSettingsWip? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => context.Designer ?? new MagicBreadcrumbDesigner(context, SettingsTyped);
}