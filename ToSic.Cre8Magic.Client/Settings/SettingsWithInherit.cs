using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Settings;

// No base interface, because for processing we need to ensure it's always a record
public abstract record SettingsWithInherit // : IInherit
{
    protected SettingsWithInherit() { }

    /// <summary>
    /// Clone support.
    /// </summary>
    protected SettingsWithInherit(SettingsWithInherit? priority, SettingsWithInherit? fallback = default)
    {
        Inherits = priority?.Inherits ?? fallback?.Inherits;
    }

    internal const string InheritsNameInJson = "@inherits";
    [JsonPropertyName(InheritsNameInJson)]
    public string? Inherits { get; init; }


    protected static int PickFirstNonZeroInt(int?[] values) => values.FirstOrDefault(v => v != null && v != 0) ?? 0;
}