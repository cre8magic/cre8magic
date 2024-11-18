using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Oqtane.Services;
using Oqtane.UI;
using ToSic.Cre8magic.Services.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;
using static Microsoft.AspNetCore.Localization.CookieRequestCultureProvider;

namespace ToSic.Cre8magic.Languages;

/*
 * Todo:
 * - make list of languages optional in the skin
 * - if supplied, it must use the order it was given in the skin params
 * - ...and only show these; possibly show more to admin?
 */

public class MagicLanguagesService(NavigationManager navigation, IJSRuntime jsRuntime, ILanguageService oqtLanguages, IMagicSettingsService settingsSvc, IMagicFactoryWip factory)
{
    public async Task<MagicLanguagesState> State(PageState pageState)
    {
        var themeContext = settingsSvc.GetThemeContext(pageState);
        var languagesSettings = ((MagicSettingsService)settingsSvc).LanguagesSettings(themeContext.ThemeSettings, themeContext.SettingsName);

        var (languages, settings) = await LanguagesToShow(pageState, languagesSettings);
        var show = themeContext.ThemeSettings.Show("Languages") && themeContext.ThemeSettings.LanguagesMin <= languages.Count;
        var designer = factory.LanguagesDesigner(pageState);
        return new(show, languages, designer) { LanguagesSettings = settings };
    }
    private GetKeepByPageId<MagicLanguagesState> _languageStates = new();

    private async Task<(List<MagicLanguage> Languages, MagicLanguagesSettings Settings)> LanguagesToShow(PageState pageState, MagicLanguagesSettings langsSettings)
    {
        var siteId = pageState.Site.SiteId;
        if (_languages.TryGetValue(siteId, out var cached))
            return cached;

        var siteLanguages = await oqtLanguages.GetLanguagesAsync(siteId);

        var customList = langsSettings.Languages.Values;

        var siteLanguageCodes = siteLanguages.Select(l => l.Code).ToList();

        // Primary order of languages. If specified, use that, otherwise use site list
        var primaryOrder = (customList.Any()
                ? customList.Select(l => l.Culture)
                : siteLanguageCodes)
            .ToList();

        if (!langsSettings.HideOthers && primaryOrder.Count < siteLanguages.Count)
        {
            var missingLanguages = siteLanguageCodes
                .Where(slc => !primaryOrder.Any(slc.EqInvariant)).ToList();
            primaryOrder = primaryOrder.Concat(missingLanguages).ToList();
        }

            // Create list with Code, Label and Title
        var result = primaryOrder
            .Select(code =>
            {
                var customLabel = customList.FirstOrDefault(l => l.Culture.EqInvariant(code));

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
        _languages[siteId] = (result, langsSettings);
        return (result, langsSettings);
    }

    private readonly Dictionary<int, (List<MagicLanguage> Languages, MagicLanguagesSettings Settings)> _languages = new();

    public async Task SetCultureAsync(string culture)
    {
        if (culture == CultureInfo.CurrentUICulture.Name) return;

        var interop = new Interop(jsRuntime);
        var localizationCookieValue = MakeCookieValue(new(culture));
        await interop.SetCookie(DefaultCookieName, localizationCookieValue, 360);

        navigation.NavigateTo(navigation.Uri, forceLoad: true);
    }
}