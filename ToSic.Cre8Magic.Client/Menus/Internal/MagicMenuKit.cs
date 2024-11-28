using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// tbd
/// </summary>
internal record MagicMenuKit : IMagicMenuKit
{
    public required IMagicPage Page { get; init; }

    public IEnumerable<IMagicPage> Pages => Page.Children;

    public required MagicMenuSettings Settings { get; init; }

    public required IMagicDesign Design { get; init; }

    // TODO:
    //public object Designer => Pages.Settings.Designer;

    public string Variant => Settings.Variant ?? "";

    public bool IsVariant(string variant) =>
        Variant.Equals(variant, StringComparison.OrdinalIgnoreCase);

    public /* actually internal */ required IContextWip Context { get; init; }

    // TODO: naming not final
    public IMagicMenuKit Kit(IMagicPage page) =>
        this with
        {
            Page = page,
            Design = new PageListDesignWip(page),
        };
}