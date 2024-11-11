using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Client.Settings;

/// <summary>
/// Simple interface to mark something which can inherit settings from elsewhere...
/// </summary>
internal interface IInherit
{
    /// <summary>
    /// Determines if it inherits another property
    /// </summary>
    [JsonPropertyName(SettingsWithInherit.InheritsNameInJson)]
    string? Inherits { get; set; }
}