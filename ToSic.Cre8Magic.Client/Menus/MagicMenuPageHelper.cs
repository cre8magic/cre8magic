using ToSic.Cre8magic.Client.Pages;

namespace ToSic.Cre8magic.Client.Menus;

internal class MagicMenuPageHelper(MagicPageFactory pageFactory): MagicPageHelperBase(pageFactory)
{
    public void Set(MagicMenuSettings settings) => _settings = settings;

    public MagicMenuSettings Settings => _settings ??= MagicMenuSettings.Defaults.Fallback;
    private MagicMenuSettings? _settings;

    protected override IPageDesigner FallbackDesigner() => new MenuDesigner(/*this,*/ Settings);
}