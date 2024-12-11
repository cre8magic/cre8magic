using ToSic.Cre8magic.Internal.Debug;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Menus;

public interface IMagicMenuKit: IHasDebugInfo
{
    /// <summary>
    /// INTERNAL context for this menu, WIP.
    /// For debugging.
    /// </summary>
    internal WorkContext WorkContext { get; }

    /// <summary>
    /// The root node to start from.
    /// This behaves like a <see cref="IMagicPage"/> but is not really a page.
    /// It does provide <see cref="IMagicPage.Children"/> and can also do design work like apply `Classes`,
    /// typically to the outermost `<ul></ul>` tags etc.
    /// </summary>
    IMagicPage Root { get; init; }

    /// <summary>
    /// The settings used to retrieve and build the pages.
    /// </summary>
    MagicMenuSpell Spell { get; }

    /// <summary>
    /// The menu variant to use.
    /// Usually determined by the settings.
    /// </summary>
    string Variant { get; }

    /// <summary>
    /// Check if the menu is of a specific variant. Case-insensitive.
    /// </summary>
    /// <param name="variant">the variant to check, like "vertical"</param>
    /// <returns></returns>
    bool IsVariant(string variant);
}