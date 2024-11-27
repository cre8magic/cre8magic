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
    /// Get the language kit. Must be async, because it might need to load data from Oqtane.
    /// </summary>
    /// <param name="pageState">The Oqtane PageState</param>
    /// <param name="settings">Settings to use - or if null, just use all the defaults</param>
    /// <returns></returns>
    Task<IMagicLanguageKit> LanguageKitAsync(PageState pageState, MagicLanguageSettings? settings = default);

    Task SetCultureAsync(string culture);
}