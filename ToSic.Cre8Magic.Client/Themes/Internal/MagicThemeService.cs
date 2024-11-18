using Oqtane.UI;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

internal class MagicThemeService(IMagicSettingsService settingsSvc) : IMagicThemeService
{
    public MagicThemeState State(PageState pageState) => _state.Get(pageState, () => BuildState(pageState));
    private readonly GetKeepByPageId<MagicThemeState> _state = new();

    private MagicThemeState BuildState(PageState pageState)
    {
        var themeContext = settingsSvc.GetThemeContext(pageState);
        var designer = new MagicThemeDesigner(themeContext);
        return new MagicThemeState(themeContext, designer);
    }
}