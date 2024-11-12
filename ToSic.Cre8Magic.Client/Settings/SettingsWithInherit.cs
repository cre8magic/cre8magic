using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

public abstract record SettingsWithInherit: IInherit
{
    internal const string InheritsNameInJson = "@inherits";
    [JsonPropertyName(InheritsNameInJson)]
    public string? Inherits { get; set; }
}