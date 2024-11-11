using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages;

public interface IMagicPageListOld
{
    int MenuLevel { get; }

    bool HasChildren { get; }

    IEnumerable<IMagicPageWithDesignWip> Children { get; }

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