using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Created as a base record, not as an interface, because for processing we need to ensure it's always a record
/// </remarks>
public abstract record MagicInheritsBase
{
    /// <summary>
    /// Empty Constructor necessary for deserialization of inheriting classes
    /// </summary>
    [PrivateApi]
    protected MagicInheritsBase() { }

    /// <summary>
    /// Clone support.
    /// </summary>
    [PrivateApi]
    protected MagicInheritsBase(MagicInheritsBase? priority, MagicInheritsBase? fallback = default)
    {
        Inherits = priority?.Inherits ?? fallback?.Inherits;
    }

    /// <summary>
    /// Name of configuration it inherits.
    /// </summary>
    [JsonPropertyName("@inherits")]
    public string? Inherits { get; init; }
}