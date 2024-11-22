using ToSic.Cre8magic.Themes.Settings;

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

    public required MagicLanguageDesigner Designer { get; init; }

    public /* actually internal */ required MagicLanguageSettings Settings { get; init; }

    //public /* actually internal */ required MagicThemeDesignSettings ThemeDesignSettings { get; init; }

    public required IMagicLanguageService Service { get; init; }

    public Task SetCultureAsync(string culture) => Service.SetCultureAsync(culture);
}