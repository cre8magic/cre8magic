using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Oqtane.Services;
using Oqtane.Shared;
using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;
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

internal class MagicLanguageService(NavigationManager navigation, IJSRuntime jsRuntime, ILanguageService oqtLangSvc, IMagicSettingsService settingsSvc) : IMagicLanguageService
{
    private const string OptionalPrefix = "language-";
    private const string DefaultPartName = "Language";

    /// <inheritdoc/>
    public async Task<IMagicLanguageKit> LanguageKitAsync(PageState pageState, MagicLanguageSettings? settings = default) =>
        await _languageStates.GetAsync(pageState, async () => await CreateState(pageState, settings));
    private readonly GetKeepByPageId<IMagicLanguageKit> _languageStates = new();
    
    private async Task<IMagicLanguageKit> CreateState(PageState pageState, MagicLanguageSettings? settings)
    {
        var (settingsFull, _, themePart, journal) = MergeSettings(pageState, settings);

        var languages = await LanguagesToShow(pageState, settingsFull);
        var show = themePart?.Show != false && settingsFull.MinLanguagesToShow <= languages.Count;
        var designer = LanguageDesigner(pageState, settingsFull);
        return new MagicLanguageKit
        {
            Show = show,
            Languages = languages,
            Designer = designer,
            Settings = settingsFull,
            Service = this,
        };
    }

    private Data3WithJournal<MagicLanguageSettings, CmThemeContext, MagicThemePartSettings?> MergeSettings(PageState pageState, MagicLanguageSettings? settings) =>
        settingsSvc.GetBestSettingsAndDesignSettings(
            pageState,
            settings,
            settingsSvc.Languages,
            settings?.DesignSettings,
            settingsSvc.LanguageDesigns,
            OptionalPrefix,
            DefaultPartName,
            finalize: (settingsData, designSettings) => new(settingsData, settings) { DesignSettings = designSettings });


    private async Task<List<MagicLanguage>> LanguagesToShow(PageState pageState, MagicLanguageSettings settings)
    {
        var siteId = pageState.Site.SiteId;
        if (_languages.TryGetValue(siteId, out var cached))
            return cached;

        var siteLanguages = await oqtLangSvc.GetLanguagesAsync(siteId);
        var siteLanguageCodes = siteLanguages.Select(l => l.Code).ToList();

        // Primary order of languages. If specified, use that, otherwise use site list
        var customList = settings.Languages.Values;
        var primaryOrder = (customList.Any()
                ? customList.Select(l => l.Culture)
                : siteLanguageCodes)
            .ToList();

        if (!settings.HideOthersSafe && primaryOrder.Count < siteLanguages.Count)
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


    public MagicLanguageDesigner LanguageDesigner(PageState pageState, MagicLanguageSettings settingsFull)
    {
        if (_languagesDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;
        var themeContext = settingsSvc.GetThemeContextFull(pageState);

        var languages = new MagicLanguageDesigner(themeContext, settingsFull);
        _languagesDesigners[pageState.Page.PageId] = languages;
        return languages;
    }
    private readonly Dictionary<int, MagicLanguageDesigner> _languagesDesigners = new();


    public async Task SetCultureAsync(string culture)
    {
        if (culture == CultureInfo.CurrentUICulture.Name) return;

        var interop = new Interop(jsRuntime);
        var localizationCookieValue = CookieRequestCultureProvider.MakeCookieValue(new(culture));
        await interop.SetCookie(CookieRequestCultureProvider.DefaultCookieName, localizationCookieValue, 360);

        navigation.NavigateTo(navigation.Uri, forceLoad: true);
    }
}