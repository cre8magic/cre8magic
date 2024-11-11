
// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

/// <summary>
/// A list of magic pages with design capabilities.
///
/// It does not represent a page itself, but a list of pages.
/// For example, a breadcrumb or a menu.
/// </summary>
public interface IMagicPageList: IEnumerable<IMagicPageWithDesignWip>
{
    int MenuLevel { get; }

    // TODO:
    //string MenuId { get; }

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
}