
// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

/// <summary>
/// A extended magic page which includes design information.
///
/// TODO: naming not final
/// </summary>
public interface IMagicPageWithDesignWip: IMagicPage
{
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