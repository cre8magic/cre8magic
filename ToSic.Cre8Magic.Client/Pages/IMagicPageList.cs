
using ToSic.Cre8magic.Pages.Internal;

namespace ToSic.Cre8magic.Pages;

/// <summary>
/// A list of magic pages with design capabilities.
///
/// It does not represent a page itself, but a list of pages.
/// For example, a breadcrumb or a menu.
///
/// It can be iterated to get the pages.
/// </summary>
public interface IMagicPageList: IEnumerable<IMagicPageWithDesignWip>
{
    /// <summary>
    /// The depth in the current menu, starting from 1.
    /// Can be different from the normal Level, since it starts at 1 (not 0)
    /// and if the menu starts at level 2 or 3, this will still be 1.
    /// </summary>
    int MenuLevel { get; }

    /// <summary>
    /// Get css class for tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    string? Classes(string tag);

    /// <summary>
    /// Get attribute value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    string? Value(string key);

    IMagicPageSetSettings Settings { get; }

    internal MagicPagesFactoryBase Factory { get; }
}