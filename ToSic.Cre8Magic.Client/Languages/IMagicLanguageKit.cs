using ToSic.Cre8magic.Internal.Debug;

namespace ToSic.Cre8magic.Languages;

/// <summary>
/// The Kit to Show language menus in the UI.
///
/// You can get it by `@inject` / `[Inject]`-ing the `IMagicHat` and calling the <see cref="IMagicHat.LanguageKitAsync"/>.
/// </summary>
public interface IMagicLanguageKit: IHasDebugInfo
{
    /// <summary>
    /// Determines if the languages should be shown. Will be retrieved from the settings
    /// </summary>
    bool Show { get; init; }

    /// <summary>
    /// List of languages to show as specified in the settings.
    /// </summary>
    IEnumerable<MagicLanguage> Languages { get; init; }


    MagicLanguageDesigner Designer { get; init; }

    MagicLanguageSettings Settings { get; init; }

    //internal MagicThemeDesignSettings ThemeDesignSettings { get; init; }

    /// <summary>
    /// Command to set the culture.
    /// This will trigger a page reload.
    /// </summary>
    /// <param name="culture"></param>
    /// <returns></returns>
    public Task SetCultureAsync(string culture);
}