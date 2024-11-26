using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts.Internal;

internal class MagicPageContextService(IMagicSettingsService settingsSvc, IMagicThemeJsService jsSvc) : IMagicPageContextService
{
    public MagicPageContextState State(PageState pageState, MagicPageContextSettings? settings) =>
        _pageContexts.Get(pageState, () => BuildState(pageState, settings));
    private readonly GetKeepByPageId<MagicPageContextState> _pageContexts = new();

    private MagicPageContextState BuildState(PageState pageState, MagicPageContextSettings? settings)
    {
        var themeCtx = settingsSvc.GetThemeContextFull(pageState);
        var useBodyTag = settings?.UseBodyTag ?? themeCtx.ThemeSettings.MagicContextInBody == true;
        var tagId = settings?.TagId ?? themeCtx.ThemeDesignSettings.MagicContextTagId;
        var themeDesigner = new MagicThemeDesigner(themeCtx);
        var contextClasses = themeDesigner.BodyClasses(themeCtx.PageTokens, settings?.Classes);
        return new(UseBodyTag: useBodyTag, Classes: contextClasses, TagId: tagId);
    }

    public async Task SetBodyClasses(PageState pageState, MagicPageContextSettings? settings)
    {
        var state = State(pageState, settings);
        if (!state.UseBodyTag)
            return;

        await jsSvc.SetBodyClasses(state.Classes ?? "");
    }
}