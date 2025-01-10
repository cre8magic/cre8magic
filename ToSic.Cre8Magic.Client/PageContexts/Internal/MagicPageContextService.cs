using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts.Internal;

internal class MagicPageContextService(IMagicSettingsService settingsSvc, IMagicThemeJsService jsSvc) : IMagicPageContextService
{
    public IMagicPageContextKit PageContextKit(PageState pageState, MagicPageContextSettings? settings) =>
        _pageContexts.Get(pageState, () => BuildKit(pageState, settings));
    private readonly GetKeepByPageId<IMagicPageContextKit> _pageContexts = new();


    private IMagicPageContextKit BuildKit(PageState pageState, MagicPageContextSettings? settings)
    {
        var (settingsData, _) = new GetSettings(settingsSvc, pageState, settings?.Name)
            .GetBest(settings, settingsSvc.PageContexts);

        var themeCtx = settingsSvc.GetThemeContextFull(pageState);

        var contextClasses = new MagicPageContextTailor(settingsData, pageState).BodyClasses(themeCtx.PageTokens, settings?.Classes);
        return new MagicPageContextKit
        {
            Classes = contextClasses,
            Settings = settingsData,
            // Internals
            PageState = pageState,
            Service = this
        };
    }

    internal async Task UpdateBodyTag(PageState pageState, MagicPageContextSettings? settings)
    {
        var state = PageContextKit(pageState, settings);
        if (!state.UseBodyTag)
            return;

        await jsSvc.SetBodyClasses(state.Classes ?? "");
    }
}