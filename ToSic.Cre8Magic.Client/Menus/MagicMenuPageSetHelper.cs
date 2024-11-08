using ToSic.Cre8magic.Client.Pages;

namespace ToSic.Cre8magic.Client.Menus;

internal class MagicMenuPageSetHelper(MagicPageFactory pageFactory): MagicPageSetHelperBase(pageFactory)
{
    public void Set(MagicMenuSettings settings) => _settings = settings;

    /// <summary>
    /// Settings - on first access takes the one given, or creates a default.
    /// </summary>
    public MagicMenuSettings Settings => _settings ??= MagicMenuSettings.Defaults.Fallback;
    private MagicMenuSettings? _settings;

    protected override IPageDesigner FallbackDesigner() => new MagicMenuDesigner(this, Settings);
}