using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts.Internal;

internal class MagicPageContextService(IMagicSpellsService spellsSvc, IMagicThemeJsService jsSvc) : IMagicPageContextService
{
    private const string OptionalPrefix = "pageContext-";
    private const string DefaultPartName = "PageContext";

    public IMagicPageContextKit PageContextKit(PageState pageState, MagicPageContextSpell? settings) =>
        _pageContexts.Get(pageState, () => BuildKit(pageState, settings));
    private readonly GetKeepByPageId<IMagicPageContextKit> _pageContexts = new();


    private IMagicPageContextKit BuildKit(PageState pageState, MagicPageContextSpell? settings)
    {
        var (settingsData, _, _, _) = MergeSettings(pageState, settings);

        var themeCtx = spellsSvc.GetThemeContextFull(pageState);

        var contextClasses = new MagicPageContextTailor(settingsData, pageState).BodyClasses(themeCtx.PageTokens, settings?.Classes);
        return new MagicPageContextKit
        {
            Classes = contextClasses,
            Spell = settingsData,
            // Internals
            PageState = pageState,
            Service = this
        };
    }

    private Data3WithJournal<MagicPageContextSpell, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicPageContextSpell? settings) =>
        spellsSvc.GetBestSpell(
            pageState,
            settings,
            spellsSvc.PageContexts,
            OptionalPrefix,
            DefaultPartName
        );




    internal async Task UpdateBodyTag(PageState pageState, MagicPageContextSpell? settings)
    {
        var state = PageContextKit(pageState, settings);
        if (!state.UseBodyTag)
            return;

        await jsSvc.SetBodyClasses(state.Classes ?? "");
    }
}