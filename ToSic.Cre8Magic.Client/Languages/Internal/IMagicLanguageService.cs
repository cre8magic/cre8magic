using Oqtane.UI;

namespace ToSic.Cre8magic.Languages.Internal;

/// <summary>
/// The service to get the language kit.
///
/// Not meant to be used externally, so it's hidden for now.
/// </summary>
public interface IMagicLanguageService
{
    /// <summary>
    /// Get the language kit.
    /// </summary>
    /// <param name="pageState">The Oqtane PageState</param>
    /// <param name="settings">Settings to use - or if null, just use all the defaults</param>
    /// <returns></returns>
    IMagicLanguageKit LanguageKit(PageState pageState, MagicLanguageSettings? settings = default);

    Task SetCultureAsync(string culture);
}