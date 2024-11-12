namespace ToSic.Cre8magic.Pages;

/// <summary>
/// Base class for page designers.
/// Can be used as a foundation for creating your custom designers.
///
/// This is a bit safer than just implementing the interface, as future enhancements
/// would appear on this class so your implementation would work even on an upgrade.
/// </summary>
public class MagicPageDesignerBase: IMagicPageDesigner
{
    /// <summary>
    /// Very basic implementation of the Classes generator.
    /// Will return null if not overriden.
    /// </summary>
    public virtual string? Classes(string tag, IMagicPage item) => null;

    /// <summary>
    /// Very basic implementation of the Value generator.
    /// Will return null if not overriden.
    /// </summary>
    public virtual string? Value(string key, IMagicPage item) => null;
}