using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

internal class MagicThemeService(IMagicSettingsService settingsSvc, ScopedDictionaryCache<IMagicThemeKit> cacheSvc) : IMagicThemeService
{
    public IMagicThemeKit ThemeKit(PageState pageState, MagicThemeSettings? settings) =>
        _state.Get(pageState, () => BuildState(pageState, settings));
    private readonly GetKeepByPageId<IMagicThemeKit> _state = new();

    private IMagicThemeKit BuildState(PageState pageState, MagicThemeSettings? settings)
    {
        var cacheId = pageState.Page.PageId.ToString();
        if (cacheSvc.TryGetValue(cacheId, out var value))
            return value;

        var themeContext = settingsSvc.GetThemeContextFull(pageState);
        var designer = new MagicThemeTailor(themeContext);
        var result = new MagicThemeKit
        {
            Context = themeContext,
            Tailor = designer,
        };
        cacheSvc[cacheId] = result;
        return result;
    }
}