using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

internal class MagicThemeService(IMagicSettingsService settingsSvc, ScopedDictionaryCache<IMagicThemeKit> cacheSvc) : IMagicThemeService
{
    public IMagicThemeKit ThemeKit(PageState pageState) => _state.Get(pageState, () => BuildState(pageState));
    private readonly GetKeepByPageId<IMagicThemeKit> _state = new();

    private IMagicThemeKit BuildState(PageState pageState)
    {
        var cacheId = pageState.Page.PageId.ToString();
        if (cacheSvc.TryGetValue(cacheId, out var value))
            return value;

        var themeContext = settingsSvc.GetThemeContextFull(pageState);
        var designer = new MagicThemeDesigner(themeContext);
        var result = new MagicThemeKit
        {
            Context = themeContext,
            Designer = designer,
        };
        cacheSvc[cacheId] = result;
        return result;
    }
}