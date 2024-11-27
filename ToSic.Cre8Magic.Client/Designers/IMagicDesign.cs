namespace ToSic.Cre8magic.Designers;

public interface IMagicDesign
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