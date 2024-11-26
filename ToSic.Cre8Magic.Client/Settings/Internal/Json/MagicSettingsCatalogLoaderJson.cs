using System.Text.Json;
using Microsoft.Extensions.Logging;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Logging;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Internal.Json;

public class MagicSettingsCatalogLoaderJson(ILogger<MagicSettingsCatalogLoaderJson> logger)
{
    public ILogger<MagicSettingsCatalogLoaderJson> Logger { get; } = logger;

    public DataWithJournal<MagicSettingsCatalog> LoadJson(MagicThemePackage themeConfig)
    {
        List<Exception> exceptions = [];

        var jsonFileName = $"{themeConfig.WwwRoot}/{themeConfig.Url}/{themeConfig.SettingsJsonFile}";
        try
        {
            var jsonString = File.ReadAllText(jsonFileName);

            var deserializeOptions = new JsonSerializerOptions(JsonMerger.GetNewOptionsForPreMerge(Logger)) {
                PropertyNameCaseInsensitive = true,
            };

            var result = JsonSerializer.Deserialize<MagicSettingsCatalog>(jsonString, deserializeOptions)!;

            // Ensure we have version set, ATM exactly 0.01
            if (Math.Abs(result.Version - 0.01) > 0.001)
                AddException(new ArgumentException($"Json {nameof(result.Version)} must be set to 0.01", nameof(result.Version)));

            if (!result.Source.HasValue() || result.Source == MagicSettingsCatalog.SourceDefault)
                return new(result with { Source = "JSON" }, new([], exceptions));

            return new(result, new([], exceptions));
        }
        catch (Exception ex)
        {
            AddException( ex);
            return new(new(), new([], exceptions));
        }

        void AddException(Exception ex)
        {
            exceptions.Add(new SettingsException($"Error loading json settings file '{themeConfig.SettingsJsonFile}'. {ex.Message}"));
        }
    }
}