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
    /// Get the state. Must be async, because it might need to load data from Oqtane.
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    Task<IMagicLanguageKit> LanguageKitAsync(PageState pageState);

    Task SetCultureAsync(string culture);
}