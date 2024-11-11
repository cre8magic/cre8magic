using ToSic.Cre8magic.Client.Breadcrumb.Settings;

namespace ToSic.Cre8magic.Client.Pages.Internal;

internal class MagicBreadcrumbSetHelper(MagicPageFactory pageFactory): MagicPageSetHelperBase(pageFactory)
{
    public void Set(MagicBreadcrumbSettings settings) => _settings = settings;

    public MagicBreadcrumbSettings Settings => _settings ??= MagicBreadcrumbSettings.Defaults.Fallback;
    private MagicBreadcrumbSettings? _settings;

    protected override IPageDesigner FallbackDesigner() => new MagicBreadcrumbDesigner(this, Settings);
}