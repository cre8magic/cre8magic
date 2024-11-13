using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Oqtane.Services;
using Oqtane.UI;
using ToSic.Cre8magic.Languages.Settings;
using ToSic.Cre8magic.Utils;
using static Microsoft.AspNetCore.Localization.CookieRequestCultureProvider;

namespace ToSic.Cre8magic.Client.Services;

/*
 * Todo:
 * - make list of languages optional in the skin
 * - if supplied, it must use the order it was given in the skin params
 * - ...and only show these; possibly show more to admin?
 */

public class LanguageService(NavigationManager navigation, IJSRuntime jsRuntime, ILanguageService oqtLanguages)
    : MagicServiceWithSettingsBase
{
    public async Task<bool> ShowMenu(int siteId)
    {
        var languages = await LanguagesToShow(siteId);
        return AllSettings.Show("Languages") && AllSettings.Theme.LanguagesMin <= languages.Count;
    }

    public async Task<List<MagicLanguage>> LanguagesToShow(int siteId)
    {
        if (_languages.TryGetValue(siteId, out var cached)) return cached;

        var siteLanguages = await oqtLanguages.GetLanguagesAsync(siteId);

        var langSettings = AllSettings.Languages;

        var customList = langSettings.Languages.Values;

        var siteLanguageCodes = siteLanguages.Select(l => l.Code).ToList();

        // Primary order of languages. If specified, use that, otherwise use site list
        var primaryOrder = (customList.Any()
                ? customList.Select(l => l.Culture)
                : siteLanguageCodes)
            .ToList();

        if (!langSettings.HideOthers && primaryOrder.Count < siteLanguages.Count)
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
        _languages[siteId] = result;
        return result;
    }

    private readonly Dictionary<int, List<MagicLanguage>> _languages = new();

    public async Task SetCultureAsync(string culture)
    {
        if (culture == CultureInfo.CurrentUICulture.Name) return;

        var interop = new Interop(jsRuntime);
        var localizationCookieValue = MakeCookieValue(new(culture));
        await interop.SetCookie(DefaultCookieName, localizationCookieValue, 360);

        navigation.NavigateTo(navigation.Uri, forceLoad: true);
    }
}