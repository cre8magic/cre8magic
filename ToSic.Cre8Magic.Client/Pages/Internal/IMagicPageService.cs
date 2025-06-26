using Oqtane.Models;
using Oqtane.UI;

namespace ToSic.Cre8magic.Pages.Internal;

public interface IMagicPageService
{
    IEnumerable<IMagicPage> GetAll(PageState pageState, bool ignorePermissions = default);

    IMagicPage GetHome(PageState pageState);

    IMagicPage GetCurrent(PageState pageState);

    IMagicPage? GetPage(PageState pageState, int pageId);

    IMagicPage? GetPage(PageState pageState, Page? page);

    IEnumerable<IMagicPage> GetPages(PageState pageState, IEnumerable<int> pageIds);
}