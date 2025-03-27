namespace ToSic.Cre8magic.Tailors;

public interface IMagicTailor
{
    /// <summary>
    /// Will retrieve the classes for specified target.
    /// </summary>
    /// <remarks>
    /// Typical use is
    ///
    /// <code language="razor">
    /// &lt;div class="@SomeKit.Tailor.Classes("div")"&gt;
    /// </code>
    /// </remarks>
    /// <param name="target">a name such as `div` or `top-wrapper`</param>
    /// <returns>the classes for that target or `null` (if null, the `class` attribute would not be generated in the html)</returns>
    public string? Classes(string target);

    public string? Value(string target);

    public string? Id(string target);
}