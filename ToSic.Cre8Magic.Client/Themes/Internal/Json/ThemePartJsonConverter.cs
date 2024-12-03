using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal.Json;

/// <summary>
/// ... 
/// </summary>
public class ThemePartJsonConverter : JsonConverter<MagicThemePartSettings>
{

    public override void Write(Utf8JsonWriter writer, MagicThemePartSettings? part, JsonSerializerOptions options)
    {
        if (part == null || (part.Show == null && part.Settings == null && part.Design == null))
        {
            writer.WriteNullValue();
            return;
        }

        // Copy options to remove this serializer, then serialize with default method
        JsonSerializer.Serialize(writer, new MagicThemePartSettings.NoJsonConverter(part), options);
    }



    public override MagicThemePartSettings? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var x = JsonNode.Parse(ref reader);
        return x switch
        {
            null => null,
            JsonArray => Dummy(),
            JsonValue jValue => ConvertValue(jValue),
            JsonObject jObject => jObject.Deserialize<MagicThemePartSettings.NoJsonConverter>(options),
            _ => Dummy(),
        };
    }

    private static MagicThemePartSettings ConvertValue(JsonValue value)
    {
        if (value.TryGetValue<string>(out var str))
        {
            var asBool = IsBoolean(str);
            return asBool != null ? new(asBool.Value) : new(str);
        }

        return value.TryGetValue<bool>(out var bln) ? new(bln) : Dummy();
    }

    private static bool? IsBoolean(string value)
    {
        if (!value.HasValue()) return null;
        if (value.EqInvariant(true.ToString())) return true;
        if (value.EqInvariant(false.ToString())) return false;
        return null;
    }

    private static MagicThemePartSettings Dummy() => new()
    {
        Show = null,
        Design = null,
        Settings = null,
    };
}
