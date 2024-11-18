using Oqtane.UI;

namespace ToSic.Cre8magic.Languages;

public interface IMagicLanguageService
{
    /// <summary>
    /// Get the state. Must be async, because it might need to load data from Oqtane.
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    Task<MagicLanguageState> GetStateAsync(PageState pageState);

    Task SetCultureAsync(string culture);
}