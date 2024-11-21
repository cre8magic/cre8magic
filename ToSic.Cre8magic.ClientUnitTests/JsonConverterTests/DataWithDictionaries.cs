using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings.Internal.Json;

namespace ToSic.Cre8magic.ClientUnitTests.JsonConverterTests;

internal class DataWithDictionaries
{
    public IDictionary<string, string>? Classic { get; set; }

    [JsonConverter(typeof(CaseInsensitiveIDictionaryConverter<string>))]
    public IDictionary<string, string>? Invariant { get; set; }

    public IDictionary<string, string>? ClassicShouldBeNull { get; set; }

    [JsonConverter(typeof(CaseInsensitiveIDictionaryConverter<string>))]
    public IDictionary<string, string>? InvariantShouldBeNull { get; set; }

    public IDictionary<string, string> ClassicShouldBeNonNull { get; set; } = new Dictionary<string, string>();

    [JsonConverter(typeof(CaseInsensitiveIDictionaryConverter<string>))]
    public IDictionary<string, string> InvariantShouldBeNonNull { get; set; } = new Dictionary<string, string>();

    public const string Json = @"
{
    ""Classic"": {
        ""Key1"": ""Value1"",
        ""Key2"": ""Value2""
    },
    ""Invariant"": {
        ""Key1"": ""Value1"",
        ""Key2"": ""Value2""
    }
}";
}