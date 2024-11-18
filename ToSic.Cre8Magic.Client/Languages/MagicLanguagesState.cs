namespace ToSic.Cre8magic.Languages;

/// <summary>
/// TODO: WIP, maybe create interface....
/// </summary>
public class MagicLanguagesState(
    bool show,
    IEnumerable<MagicLanguage> languages,
    MagicLanguageDesigner designer)
{
    /// <summary>
    /// Determines if the languages should be shown. Will be retrieved from the settings
    /// </summary>
    public bool Show { get; init; } = show;

    public IEnumerable<MagicLanguage> Languages { get; init; } = languages;

    public MagicLanguageDesigner Designer { get; init; } = designer;

    internal MagicLanguagesSettings LanguagesSettings { get; init; }
}