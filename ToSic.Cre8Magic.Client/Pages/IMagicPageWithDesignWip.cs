

using ToSic.Cre8magic.Client.Pages;

// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

/// <summary>
/// A extended magic page which includes design information.
///
/// TODO: naming not final
/// </summary>
public interface IMagicPageWithDesignWip: IMagicPage, IMagicPageListOld
{
    /// <summary>
    /// Get css class for tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    new string? Classes(string tag);

    /// <summary>
    /// Get attribute value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    new string? Value(string key);

    new IEnumerable<IMagicPageWithDesignWip> Children { get; }

    new bool HasChildren { get; }

    new int MenuLevel { get; }
}