using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Spells.Values;

namespace ToSic.Cre8magic.Spells.Internal.Json;

/// <summary>
/// Custom converter for <see cref="MagicSettingOnOff"/> to handle different JSON formats.
/// </summary>
internal class SettingOnOffJsonConverter : JsonConverter<MagicSettingOnOff>
{

    public override void Write(Utf8JsonWriter writer, MagicSettingOnOff? pair, JsonSerializerOptions options)
    {
        if (pair?.On == null && pair?.Off == null)
        {
            writer.WriteNullValue();
            return;
        }

        // Copy options to remove this serializer, then serialize with default method
        JsonSerializer.Serialize(writer, new MagicSettingOnOff.NoJsonConverter(pair), options);
    }

    public override MagicSettingOnOff? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var x = JsonNode.Parse(ref reader);
        return x switch
        {
            null => null,
            JsonArray jArray => ConvertArray(jArray),
            JsonValue jValue => new() { On = jValue.ToString() },
            JsonObject jObject => jObject.Deserialize<MagicSettingOnOff.NoJsonConverter>(options),
            _ => new() { On = "error", Off = "error" },
        };
    }

    private static MagicSettingOnOff? ConvertArray(JsonArray jsonArray) =>
        jsonArray.Count == 0
            ? null
            : new()
            {
                On = jsonArray[0]?.ToString(),
                Off = jsonArray.Count > 1 ? jsonArray[1]?.ToString() : null
            };
}
