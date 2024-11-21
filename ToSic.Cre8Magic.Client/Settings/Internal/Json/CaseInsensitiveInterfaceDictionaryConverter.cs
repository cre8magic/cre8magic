using System.Text.Json.Serialization;
using System.Text.Json;

namespace ToSic.Cre8magic.Settings.Internal.Json;

/// <summary>
/// Inspired by https://stackoverflow.com/a/67308143/5044294
/// </summary>
/// <typeparam name="TValue"></typeparam>
internal sealed class CaseInsensitiveInterfaceDictionaryConverter<TValue>
    : JsonConverter<IDictionary<string, TValue>>
{
    public override IDictionary<string, TValue>? Read(
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
        IDictionary<string, TValue> value,
        JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}