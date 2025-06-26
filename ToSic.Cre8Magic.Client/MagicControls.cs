namespace ToSic.Cre8magic;

/// <summary>
/// List of all built in Magic Controls.
/// It is used for certain APIs which will request resources for specific controls.
/// </summary>
[Flags]
public enum MagicControls
{
    /// <summary>
    /// Fallback / default if not set.
    /// </summary>
    Unknown = 0x0,

    /// <summary>
    /// All controls.
    /// </summary>
    All = 0x1,

    /// <summary>
    /// The "To Top" control.
    /// </summary>
    ToTop = 0x2,
}