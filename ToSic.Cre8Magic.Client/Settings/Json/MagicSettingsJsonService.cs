using System.Text.Json;
using Microsoft.Extensions.Logging;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Json;

public class MagicSettingsJsonService(ILogger<MagicSettingsJsonService> logger)
{
    public ILogger<MagicSettingsJsonService> Logger { get; } = logger;

    public MagicSettingsCatalog LoadJson(MagicPackageSettings themeConfig)
    {
        var jsonFileName = $"{themeConfig.WwwRoot}/{themeConfig.Url}/{themeConfig.SettingsJsonFile}";
        try
        {
            var jsonString = File.ReadAllText(jsonFileName);
                
            var result = JsonSerializer.Deserialize<MagicSettingsCatalog>(jsonString, new JsonSerializerOptions(JsonMerger.GetNewOptionsForPreMerge(Logger))
            {
                PropertyNameCaseInsensitive = true,
            })!;

            // Ensure we have version set, ATM exactly 0.01
            if (Math.Abs(result.Version - 0.01) > 0.001)
                AddException(themeConfig, new ArgumentException($"Json {nameof(result.Version)} must be set to 0.01", nameof(result.Version)));

            if (!result.Source.HasValue() || result.Source == MagicSettingsCatalog.SourceDefault)
                return result with { Source = "JSON", Logs = new(Exceptions) };

            return result with { Logs = new(Exceptions) };
        }
        catch (Exception ex)
        {
            AddException(themeConfig, ex);
            return new()
            {
                Logs = new(Exceptions)
            };
        }
    }

    public List<Exception> Exceptions { get; } = new();

    private void AddException(MagicPackageSettings themeConfig, Exception ex)
    {
        Exceptions.Add(new SettingsException($"Error loading json configuration file '{themeConfig.SettingsJsonFile}'. {ex.Message}"));
    }
}