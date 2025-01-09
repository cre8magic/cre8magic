using Oqtane.UI;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts.Internal;

internal class MagicPageContextService(IMagicSpellsService spellsSvc, IMagicThemeJsService jsSvc) : IMagicPageContextService
{
    public IMagicPageContextKit PageContextKit(PageState pageState, MagicPageContextSpell? settings) =>
        _pageContexts.Get(pageState, () => BuildKit(pageState, settings));
    private readonly GetKeepByPageId<IMagicPageContextKit> _pageContexts = new();


    private IMagicPageContextKit BuildKit(PageState pageState, MagicPageContextSpell? settings)
    {
        var (settingsData, _) = new GetSpell(spellsSvc, pageState, settings?.Name)
            .GetBestSpell(settings, spellsSvc.PageContexts);

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

    internal async Task UpdateBodyTag(PageState pageState, MagicPageContextSpell? settings)
    {
        var state = PageContextKit(pageState, settings);
        if (!state.UseBodyTag)
            return;

        await jsSvc.SetBodyClasses(state.Classes ?? "");
    }
}