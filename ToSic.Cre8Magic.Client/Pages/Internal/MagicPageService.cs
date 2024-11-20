using Oqtane.Models;
using Oqtane.UI;

namespace ToSic.Cre8magic.Pages.Internal;

internal class MagicPageService : IMagicPageService
{
    public IEnumerable<IMagicPage> GetAll(PageState pageState, bool ignorePermissions = default) =>
        ignorePermissions
            ? new MagicPageFactory(pageState).PagesAll()
            : new MagicPageFactory(pageState).PagesUser();

    public IMagicPage GetHome(PageState pageState) =>
        new MagicPageFactory(pageState).Home;

    public IMagicPage GetCurrent(PageState pageState) =>
        new MagicPageFactory(pageState).Current;

    public IMagicPage? GetPage(PageState pageState, int pageId) =>
        new MagicPageFactory(pageState).GetOrNull(pageId);

    public IMagicPage? GetPage(PageState pageState, Page? page) =>
        new MagicPageFactory(pageState).CreateOrNull(page);


    public IEnumerable<IMagicPage> GetPages(PageState pageState, IEnumerable<int> pageIds) =>
        new MagicPageFactory(pageState).Get(pageIds);

}