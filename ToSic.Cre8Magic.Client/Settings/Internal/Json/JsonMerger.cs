using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Settings.Internal.Json;

/// <summary>
/// Inspired by https://github.com/dotnet/runtime/issues/31433
/// </summary>
internal class JsonMerger
{
    public static JsonSerializerOptions GetNewOptionsForPreMerge(ILogger logger) => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        Converters =
        {
            PairOnOffJsonConverter.GetNew(logger),
            DesignSettingsJsonConverter<MagicBlueprintPart>.GetNew(logger),
            DesignSettingsJsonConverter<MagicMenuBlueprintPart>.GetNew(logger),
            ThemePartJsonConverter.GetNew(logger),
        },
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };
}