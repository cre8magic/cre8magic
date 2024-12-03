using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Settings.Internal.Json;

/// <summary>
/// Inspired by https://github.com/dotnet/runtime/issues/31433
/// </summary>
internal class JsonMerger
{
    public static JsonSerializerOptions GetNewOptionsForPreMerge() => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };
}