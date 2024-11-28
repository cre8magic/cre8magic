using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// tbd
/// </summary>
internal record MagicMenuKit : IMagicMenuKit
{
    public required IMagicPage Root { get; init; }

    public required MagicMenuSettings Settings { get; init; }

    // TODO:
    //public object Designer => Pages.Settings.Designer;

    /// <summary>
    /// The variant - never null; defaults to ...
    /// </summary>
    public string Variant => Settings.Variant ?? "";

    public bool IsVariant(string variant) =>
        Variant.Equals(variant, StringComparison.OrdinalIgnoreCase);

    public /* actually internal */ required WorkContext WorkContext { get; init; }

}