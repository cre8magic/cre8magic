using ToSic.Cre8magic.Breadcrumb.Settings;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Pages.Internal.Breadcrumb;

internal class MagicBreadcrumbFactory(ContextWip<MagicBreadcrumbSettings, IMagicPageDesigner> context) : MagicPagesFactoryBase(context)
{
    public MagicBreadcrumbSettings SettingsTyped => _settings ??= context.Settings;
    private MagicBreadcrumbSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => context.Designer ?? new MagicBreadcrumbDesigner(this, SettingsTyped);
}