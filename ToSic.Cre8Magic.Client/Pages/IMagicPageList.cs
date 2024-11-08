namespace ToSic.Cre8magic.Client.Pages;

public interface IMagicPageList
{
    int MenuLevel { get; }

    bool HasChildren { get; }

    IList<MagicMenuPage> Children { get; }

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