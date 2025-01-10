using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Internal.Json;

internal class MagicBookLoaderJson
{
    public DataWithJournal<MagicBook> LoadJson(MagicThemePackage themeConfig)
    {
        List<Exception> exceptions = [];

        var jsonFileName = $"{themeConfig.WwwRoot}/{themeConfig.Url}/{themeConfig.SettingsJsonFile}";
        try
        {
            var jsonString = File.ReadAllText(jsonFileName);

            var deserializeOptions = GetNewOptionsForPreMerge;

            var result = JsonSerializer.Deserialize<MagicBook>(jsonString, deserializeOptions)!;

            // Ensure we have version set, ATM exactly 0.01
            if (Math.Abs(result.Version - 0.01) > 0.001)
                AddException(new ArgumentException($"Json {nameof(result.Version)} must be set to 0.01", nameof(result.Version)));

            if (!result.Source.HasValue() || result.Source == MagicBook.SourceDefault)
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
            exceptions.Add(new Exception($"Error loading json settings file '{themeConfig.SettingsJsonFile}'. {ex.Message}"));
        }
    }

    [field: AllowNull, MaybeNull]
    public static JsonSerializerOptions GetNewOptionsForPreMerge => field??= new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
    };

}