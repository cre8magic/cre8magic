using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// tbd
/// </summary>
public record MagicMenuKit : IMagicMenuKit
{
    public required IMagicPageList Pages { get; init; }

    public required IMagicPageSetSettings Settings { get; init; }

    // TODO:
    //public object Designer => Pages.Settings.Designer;

    public string Variant => Pages.Settings.Variant;

    /// <summary>
    /// note: actually internal on interface
    /// </summary>
    public IContextWip Context { get; }

    public IMagicMenuKit Kit(IMagicPageList? pages) =>
        new MagicMenuKit
        {
            Pages = pages ?? Pages,
            Settings = Settings,
        };
}