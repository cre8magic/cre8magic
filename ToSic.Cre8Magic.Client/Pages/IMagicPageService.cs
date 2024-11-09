using Oqtane.UI;

// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

public interface IMagicPageService
{
    /// <summary>
    /// The page state - must be initialized before using the service.
    /// </summary>
    /// <remarks>
    /// Will throw an error if accessed before initializing.
    /// </remarks>
    PageState PageState { get; }

    IMagicPageService AddState(PageState pageState);

    IEnumerable<IMagicPage> All { get; }

    IMagicPage Home { get; }

    IMagicPage Current { get; }

    IMagicPage? Get(int pageId);

    IEnumerable<IMagicPage> Get(IEnumerable<int> pageIds);
}