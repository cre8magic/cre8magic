using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Settings.Internal.Json;

/// <summary>
/// ...
/// </summary>
internal class BlueprintPartJsonConverter : JsonConverter<MagicBlueprintPart>
{
    public override void Write(Utf8JsonWriter writer, MagicBlueprintPart? part, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, new MagicBlueprintPart.NoJsonConverter(part), options);


    public override MagicBlueprintPart? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonNode = JsonNode.Parse(ref reader);

        const string errArray = "Error unexpected data - array instead of string or object";
        return jsonNode switch
        {
            null => null,
            JsonArray _ => ConvertValue(errArray),
            JsonValue jValue => ConvertValue(jValue.ToString()),
            JsonObject jObject => jObject.Deserialize<MagicBlueprintPart.NoJsonConverter>(options),
            _ => null,
        };
    }

    private static MagicBlueprintPart ConvertValue(string value) => new() { Classes = value, Value = value };
}
