using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Internal.Json;

/// <summary>
/// Inspired by https://stackoverflow.com/a/67308143/5044294
/// </summary>
/// <typeparam name="TValue"></typeparam>
internal sealed class CaseInsensitiveDictionaryConverter<TValue>
    : JsonConverter<Dictionary<string, TValue>>
{
    public override Dictionary<string, TValue>? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var original = JsonSerializer
            .Deserialize(ref reader, typeToConvert, options);

        var dic = (Dictionary<string, TValue>?)original;

        return dic == null
            ? null
            : new Dictionary<string, TValue>(dic, StringComparer.InvariantCultureIgnoreCase);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Dictionary<string, TValue> value,
        JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}