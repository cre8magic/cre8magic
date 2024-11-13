using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

public abstract record SettingsWithInherit: IInherit
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
    public string? Inherits { get; set; }

}