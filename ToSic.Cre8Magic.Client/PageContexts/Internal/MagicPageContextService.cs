using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts.Internal;

internal class MagicPageContextService(IMagicSettingsService settingsSvc, IMagicThemeJsService jsSvc) : IMagicPageContextService
{
    public IMagicPageContextKit PageContextKit(PageState pageState, MagicPageContextSettings? settings) =>
        _pageContexts.Get(pageState, () => BuildState(pageState, settings));
    private readonly GetKeepByPageId<IMagicPageContextKit> _pageContexts = new();


    private IMagicPageContextKit BuildState(PageState pageState, MagicPageContextSettings? settings)
    {
        var themeCtx = settingsSvc.GetThemeContextFull(pageState);
        var useBodyTag = settings?.UseBodyTag ?? themeCtx.ThemeSettings.UseBodyTag == true;
        var tagId = settings?.TagId ?? themeCtx.ThemeDesignSettings.MagicContextTagId;
        var contextClasses = new MagicPageContextDesigner(themeCtx).BodyClasses(themeCtx.PageTokens, settings?.Classes);
        return new MagicPageContextKit
        {
            Classes = contextClasses,
            UseBodyTag = useBodyTag,
            Settings = settings,
            TagId = tagId,
            // Internals
            PageState = pageState,
            Service = this
        };
    }

    public async Task UpdateBodyTag(PageState pageState, MagicPageContextSettings? settings)
    {
        var state = PageContextKit(pageState, settings);
        if (!state.UseBodyTag)
            return;

        await jsSvc.SetBodyClasses(state.Classes ?? "");
    }
}