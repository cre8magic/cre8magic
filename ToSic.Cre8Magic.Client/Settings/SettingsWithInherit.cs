using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Client.Settings;

public abstract record SettingsWithInherit: IInherit
{
    internal const string InheritsNameInJson = "@inherits";
    [JsonPropertyName(InheritsNameInJson)]
    public string? Inherits { get; set; }
}