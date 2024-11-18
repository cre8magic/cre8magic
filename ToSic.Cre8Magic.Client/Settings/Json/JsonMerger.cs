using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using ToSic.Cre8magic.Menus;

namespace ToSic.Cre8magic.Settings.Json;

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
            DesignSettingsJsonConverter<MagicDesignSettings>.GetNew(logger),
            // DesignSettingsJsonConverter<DesignSettingActive>.GetNew(),
            DesignSettingsJsonConverter<MagicMenuDesignSettings>.GetNew(logger),
            // DesignSettingsJsonConverter<MagicContainerDesignSettingsItem>.GetNew(),
            ThemePartJsonConverter.GetNew(logger),
        },
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };
}