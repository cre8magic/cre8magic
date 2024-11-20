namespace ToSic.Cre8magic.Languages;

public interface IMagicLanguageKit
{
    /// <summary>
    /// Determines if the languages should be shown. Will be retrieved from the settings
    /// </summary>
    bool Show { get; init; }

    IEnumerable<MagicLanguage> Languages { get; init; }
    MagicLanguageDesigner Designer { get; init; }
    internal MagicLanguageSettings LanguageSettings { get; init; }
    internal MagicThemeDesignSettings ThemeDesignSettings { get; init; }
}