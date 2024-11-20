using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Menus;

public interface IMagicMenuKit
{
    IMagicPageList Pages { get; init; }
    IMagicPageSetSettings Settings { get; }
    string Variant { get; }

    internal IContextWip Context { get; }

    IMagicMenuKit Kit(IMagicPageList pages);
}