using Oqtane.UI;

namespace ToSic.Cre8magic.Utils;

public class GetKeepByPageId<T> where T : class
{
    public T Get(PageState pageState, Func<T> create)
    {
        return _cache.Get(
            () => (create(), pageState.Page.PageId),
            (_, i) => i == pageState.Page.PageId
        );
    }

    private readonly GetKeep<T, int?> _cache = new();
}