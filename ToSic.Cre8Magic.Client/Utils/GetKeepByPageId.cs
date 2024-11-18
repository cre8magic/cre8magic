using Oqtane.UI;

namespace ToSic.Cre8magic.Utils;

public class GetKeepByPageId<T> where T : class
{
    public T Get(PageState pageState, Func<T> create)
    {
        if (pageState == null)
            throw new ArgumentNullException(nameof(pageState),
                $"This code was called to early, before {nameof(PageState)} was ready. Make sure you only access this in output code, not in constructors etc.");

        return _cache.Get(
            () => (create(), pageState.Page.PageId),
            (_, i) => i == pageState.Page.PageId
        );
    }

    private readonly GetKeep<T, int?> _cache = new();

    // Create the same method as above, but Async

    public async Task<T> GetAsync(PageState pageState, Func<Task<T>> create)
    {
        if (pageState == null)
            throw new ArgumentNullException(nameof(pageState),
                $"This code was called to early, before {nameof(PageState)} was ready. Make sure you only access this in output code, not in constructors etc.");
        return await _cache.GetAsync(
            async () => (await create(), pageState.Page.PageId),
            (_, i) => i == pageState.Page.PageId
        );
    }
}