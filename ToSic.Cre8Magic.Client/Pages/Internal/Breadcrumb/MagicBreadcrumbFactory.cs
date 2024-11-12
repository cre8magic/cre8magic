using ToSic.Cre8magic.Breadcrumb.Settings;

namespace ToSic.Cre8magic.Pages.Internal.Breadcrumb;

internal class MagicBreadcrumbFactory(MagicPageFactory pageFactory): MagicPagesFactoryBase(pageFactory)
{
    public void Set(MagicBreadcrumbSettings settings) => _settings = settings;

    public MagicBreadcrumbSettings SettingsTyped => _settings ??= MagicBreadcrumbSettings.Defaults.Fallback;
    private MagicBreadcrumbSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IMagicPageDesigner FallbackDesigner() => new MagicBreadcrumbDesigner(this, SettingsTyped);
}