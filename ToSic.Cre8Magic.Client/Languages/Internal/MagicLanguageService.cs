using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Oqtane.Shared;
using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages.Internal;

/*
 * Todo:
 * - make list of languages optional in the skin
 * - if supplied, it must use the order it was given in the skin params
 * - ...and only show these; possibly show more to admin?
 */

internal class MagicLanguageService(NavigationManager navigation, IJSRuntime jsRuntime, IMagicSpellsService spellsSvc) : IMagicLanguageService
{
    /// <inheritdoc/>
    public IMagicLanguageKit LanguageKit(PageState pageState, MagicLanguageSpell? settings = default) =>
        _languageStates.Get(pageState, () => CreateState(pageState, settings));
    private readonly GetKeepByPageId<IMagicLanguageKit> _languageStates = new();
    
    private IMagicLanguageKit CreateState(PageState pageState, MagicLanguageSpell? settings)
    {
        var (settingsFull, themePart, journal) = MergeSettings(pageState, settings);

        var languages = LanguagesToShow(pageState, settingsFull);
        var show = themePart?.Show != false && settingsFull.MinLanguagesToShow <= languages.Count;
        var designer = Tailor(pageState, settingsFull);
        return new MagicLanguageKit
        {
            Show = show,
            Languages = languages,
            Tailor = designer,
            Spell = settingsFull,
            Service = this,
        };
    }

    private Data2WithJournal<MagicLanguageSpell, MagicThemePartSettings?> MergeSettings(
        PageState pageState, MagicLanguageSpell? settings)
    {
        var getSpell = new GetSpell(spellsSvc, pageState, settings?.Name);
        var spell = getSpell.GetBestSpell(settings, spellsSvc.Languages);
        var bluePrint = getSpell.GetBestSpell(settings?.Blueprint, spellsSvc.LanguageBlueprints, ThemePartSectionEnum.Design);
        return new(spell.Data with { Blueprint = bluePrint.Data }, getSpell.Part, spell.Journal.With(bluePrint.Journal));
    }


    private List<MagicLanguage> LanguagesToShow(PageState pageState, MagicLanguageSpell spell)
    {
        var siteId = pageState.Site.SiteId;
        if (_languages.TryGetValue(siteId, out var cached))
            return cached;

        var siteLanguages = pageState.Languages; // await oqtLangSvc.GetLanguagesAsync(siteId);
        var siteLanguageCodes = siteLanguages.Select(l => l.Code).ToList();

        // Primary order of languages. If specified, use that, otherwise use site list
        var customList = spell.Languages?.Values;
        var primaryOrder = (customList is { Count: > 0 }
                ? customList.Select(l => l.Culture)
                : siteLanguageCodes)
            .ToList();

        if (!spell.HideOthersSafe && primaryOrder.Count < siteLanguages.Count)
        {
            var missingLanguages = siteLanguageCodes
                .Where(slc => !primaryOrder.Any(slc.EqInvariant)).ToList();
            primaryOrder = primaryOrder.Concat(missingLanguages).ToList();
        }

            // Create list with Code, Label and Title
        var result = primaryOrder
            .Select(code =>
            {
                var customLabel = customList?.FirstOrDefault(l => l.Culture.EqInvariant(code));

                var langInSite = siteLanguages.Find(al => al.Code.EqInvariant(code));
                return new MagicLanguage
                {
                    Culture = code, 
                    Label = customLabel?.Label ?? code[..2].ToUpperInvariant(), 
                    Description = customLabel?.Description ?? langInSite?.Name ?? ""
                };
            })
            .Where(set => set.Description.HasValue())
            .ToList();
        _languages[siteId] = result;
        return result;
    }

    private readonly Dictionary<int, List<MagicLanguage>> _languages = new();


    public MagicLanguageTailor Tailor(PageState pageState, MagicLanguageSpell spellFull)
    {
        var themeContext = spellsSvc.GetThemeContextFull(pageState);
        var languages = new MagicLanguageTailor(themeContext, spellFull);
        return languages;
    }


    public async Task SetCultureAsync(string culture)
    {
        if (culture == CultureInfo.CurrentUICulture.Name) return;

        var interop = new Interop(jsRuntime);
        var localizationCookieValue = CookieRequestCultureProvider.MakeCookieValue(new(culture));
        await interop.SetCookie(CookieRequestCultureProvider.DefaultCookieName, localizationCookieValue, 360);

        navigation.NavigateTo(navigation.Uri, forceLoad: true);
    }
}