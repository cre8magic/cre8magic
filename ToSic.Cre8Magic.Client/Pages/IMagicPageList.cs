namespace ToSic.Cre8magic.Client.Pages;

public interface IMagicPageList
{
    bool HasChildren { get; }

    public IList<MagicMenuPage> Children { get; }

    /// <summary>
    /// Get css class for tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public string? Classes(string tag);

    /// <summary>
    /// Get attribute value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string? Value(string key);

}