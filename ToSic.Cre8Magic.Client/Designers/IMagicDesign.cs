namespace ToSic.Cre8magic.Designers;

public interface IMagicDesign
{
    /// <summary>
    /// Get css class for something - typically a tag.
    /// </summary>
    /// <param name="tagOrId"></param>
    /// <returns></returns>
    string? Classes(string tagOrId);

    /// <summary>
    /// Get some value - often for an attribute or something similar.
    /// </summary>
    /// <param name="tagOrKey"></param>
    /// <returns></returns>
    string? Value(string tagOrKey);
}