using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

internal class MagicThemeService(IMagicSpellsService spellsSvc, ScopedDictionaryCache<IMagicThemeKit> cacheSvc) : IMagicThemeService
{
    public IMagicThemeKit ThemeKit(PageState pageState, MagicThemeSpell? settings) =>
        _state.Get(pageState, () => BuildState(pageState, settings));
    private readonly GetKeepByPageId<IMagicThemeKit> _state = new();

    private IMagicThemeKit BuildState(PageState pageState, MagicThemeSpell? settings)
    {
        var cacheId = pageState.Page.PageId.ToString();
        if (cacheSvc.TryGetValue(cacheId, out var value))
            return value;

        var themeContext = spellsSvc.GetThemeContextFull(pageState);
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