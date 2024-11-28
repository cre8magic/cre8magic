using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Menus;

public interface IMagicMenuKit
{
    /// <summary>
    /// INTERNAL context for this menu, WIP.
    /// For debugging.
    /// </summary>
    internal IContextWip Context { get; }

    /// <summary>
    /// List of pages to show at this level of the menu.
    /// This is a magic page list, which means it contains magic pages and some more features.
    /// 
    /// Can contain child pages.
    /// </summary>
    IMagicPageList Pages { get; init; }

    /// <summary>
    /// The settings used to retrieve and build the pages.
    /// </summary>
    MagicMenuSettings Settings { get; }

    /// <summary>
    /// The menu variant to use.
    /// Usually determined by the settings.
    /// </summary>
    string Variant { get; }

    IMagicDesign Design { get; init; }

    IMagicMenuKit Kit(IMagicPageWithDesignWip page);

    /// <summary>
    /// Check if the menu is of a specific variant. Case-insensitive.
    /// </summary>
    /// <param name="variant">the variant to check, like "vertical"</param>
    /// <returns></returns>
    bool IsVariant(string variant);
}