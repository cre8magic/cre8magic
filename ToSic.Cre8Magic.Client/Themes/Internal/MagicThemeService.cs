using Oqtane.UI;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

internal class MagicThemeService(IMagicSettingsService settingsSvc, ScopedDictionaryCache<MagicThemeState> cacheSvc) : IMagicThemeService
{
    public MagicThemeState State(PageState pageState) => _state.Get(pageState, () => BuildState(pageState));
    private readonly GetKeepByPageId<MagicThemeState> _state = new();

    private MagicThemeState BuildState(PageState pageState)
    {
        var cacheId = pageState.Page.PageId.ToString();
        if (cacheSvc.TryGetValue(cacheId, out var value))
            return value;

        buildCount++;
        var themeContext = settingsSvc.GetThemeContext(pageState);
        var designer = new MagicThemeDesigner(themeContext);
        var result = new MagicThemeState(themeContext, designer);
        cacheSvc[cacheId] = result;
        return result;
    }

    private static int buildCount;
}