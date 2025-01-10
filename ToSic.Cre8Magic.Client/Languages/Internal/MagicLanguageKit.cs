using ToSic.Cre8magic.Internal.Debug;

namespace ToSic.Cre8magic.Languages.Internal;

/// <summary>
/// </summary>
internal record MagicLanguageKit : IMagicLanguageKit
{
    /// <summary>
    /// Determines if the languages should be shown. Will be retrieved from the settings
    /// </summary>
    public required bool Show { get; init; }

    public required IEnumerable<MagicLanguage> Languages { get; init; }

    public required MagicLanguageTailor Tailor { get; init; }

    public /* actually internal */ required MagicLanguageSettings Settings { get; init; }

    //public /* actually internal */ required MagicThemeDesignSettings ThemeDesignSettings { get; init; }

    public required IMagicLanguageService Service { get; init; }

    public Task SetCultureAsync(string culture) => Service.SetCultureAsync(culture);

    public DebugInfo GetDebugInfo()
    {
        var debugInfo = new DebugInfo
        {
            Title = "Language Settings (Debug)",
            More = new()
            {
                { "Language Settings", Settings },
                { "Languages To Show", Languages },
                { "Language Design", Settings.Blueprint },
            },
            Settings = Settings,
            Values = new()
            {
                { "Name", DebugInfo.ShowNotSet(Settings.Name) },
                { "Show", Show + "" },
            }
        };
        return debugInfo;

    }
}