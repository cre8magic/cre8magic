namespace ToSic.Cre8magic.Languages.Internal;

/// <summary>
/// TODO: WIP, maybe create interface....
/// </summary>
internal record MagicLanguageKit : IMagicLanguageKit
{
    /// <summary>
    /// Determines if the languages should be shown. Will be retrieved from the settings
    /// </summary>
    public required bool Show { get; init; }

    public required IEnumerable<MagicLanguage> Languages { get; init; }

    public required MagicLanguageDesigner Designer { get; init; }

    public /* actually internal */ required MagicLanguageSettings LanguageSettings { get; init; }

    public /* actually internal */ required MagicThemeDesignSettings ThemeDesignSettings { get; init; }
}