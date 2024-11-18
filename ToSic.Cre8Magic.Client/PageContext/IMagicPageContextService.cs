using Oqtane.UI;

namespace ToSic.Cre8magic.PageContext;

public interface IMagicPageContextService
{
    MagicPageContextState State(PageState pageState, MagicPageContextSettings? settings = default);
    Task SetBodyClasses(PageState pageState, MagicPageContextSettings? settings);
}