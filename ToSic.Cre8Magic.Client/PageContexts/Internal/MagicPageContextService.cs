using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts.Internal;

internal class MagicPageContextService(IMagicSettingsService settingsSvc, IMagicThemeJsService jsSvc) : IMagicPageContextService
{
    private const string OptionalPrefix = "pageContext-";
    private const string DefaultPartName = "PageContext";

    public IMagicPageContextKit PageContextKit(PageState pageState, MagicPageContextSettings? settings) =>
        _pageContexts.Get(pageState, () => BuildKit(pageState, settings));
    private readonly GetKeepByPageId<IMagicPageContextKit> _pageContexts = new();


    private IMagicPageContextKit BuildKit(PageState pageState, MagicPageContextSettings? settings)
    {
        var (settingsData, _, _, _) = MergeSettings(pageState, settings);
        var settingsFull = settingsData; // new MagicPageContextSettings(settingsData, settings);

        var themeCtx = settingsSvc.GetThemeContextFull(pageState);

        var contextClasses = new MagicPageContextDesigner(settingsFull, pageState).BodyClasses(themeCtx.PageTokens, settings?.Classes);
        return new MagicPageContextKit
        {
            Classes = contextClasses,
            Settings = settingsFull,
            // Internals
            PageState = pageState,
            Service = this
        };
    }

    private Data3WithJournal<MagicPageContextSettings, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicPageContextSettings? settings) =>
        settingsSvc.GetBestSettings(
            pageState,
            settings,
            settingsSvc.PageContexts,
            OptionalPrefix,
            DefaultPartName
        );




    internal async Task UpdateBodyTag(PageState pageState, MagicPageContextSettings? settings)
    {
        var state = PageContextKit(pageState, settings);
        if (!state.UseBodyTag)
            return;

        await jsSvc.SetBodyClasses(state.Classes ?? "");
    }
}