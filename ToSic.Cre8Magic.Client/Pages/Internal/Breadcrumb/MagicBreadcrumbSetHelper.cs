using ToSic.Cre8magic.Client.Breadcrumb.Settings;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal.Breadcrumb;

internal class MagicBreadcrumbSetHelper(MagicPageFactory pageFactory): MagicPageSetHelperBase(pageFactory)
{
    public void Set(MagicBreadcrumbSettings settings) => _settings = settings;

    public MagicBreadcrumbSettings SettingsTyped => _settings ??= MagicBreadcrumbSettings.Defaults.Fallback;
    private MagicBreadcrumbSettings? _settings;

    public override IMagicPageSetSettings Settings => SettingsTyped;

    protected override IPageDesigner FallbackDesigner() => new MagicBreadcrumbDesigner(this, SettingsTyped);
}