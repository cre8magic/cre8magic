using ToSic.Cre8magic.Client.Breadcrumbs.Settings;
using ToSic.Cre8magic.Client.Pages;

namespace ToSic.Cre8magic.Client.Breadcrumbs;

internal class MagicBreadcrumbSetHelper(MagicPageFactory pageFactory): MagicPageSetHelperBase(pageFactory)
{
    public void Set(MagicBreadcrumbSettings settings) => _settings = settings;

    public MagicBreadcrumbSettings Settings => _settings ??= MagicBreadcrumbSettings.Defaults.Fallback;
    private MagicBreadcrumbSettings? _settings;

    protected override IPageDesigner FallbackDesigner() => new MagicBreadcrumbDesigner(Settings);
}