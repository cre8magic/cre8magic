using ToSic.Cre8magic.Breadcrumb.Settings;

namespace ToSic.Cre8magic.Pages.Internal.Breadcrumb;

internal class MagicBreadcrumbFactory(MagicPageFactory pageFactory, MagicBreadcrumbSettings settings) : MagicPagesFactoryBase(pageFactory)
{
    public MagicBreadcrumbSettings SettingsTyped => _settings ??= settings;
    private MagicBreadcrumbSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => settings.Designer ?? new MagicBreadcrumbDesigner(this, SettingsTyped);
}