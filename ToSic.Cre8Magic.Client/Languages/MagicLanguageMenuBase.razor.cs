using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Languages.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

public abstract class MagicLanguageMenuBase: MagicControl
{
    [Inject] protected LanguageService LanguageService { get; set; }

    public List<MagicLanguage> Languages { get; private set; }

    protected override IMagicDesigner Designer => _designer ??= new LanguagesDesigner(AllSettings);
    private IMagicDesigner? _designer;

    /// <summary>
    /// Determines if the languages should be shown. Will be retrieved from the settings
    /// </summary>
    protected bool? Show { get; private set; } = null;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        LanguageService.InitSettings(AllSettings);

        // Load defined language list. It changes unless the page is reloaded, so we can cache it on this control
        Languages ??= await LanguageService.LanguagesToShow(PageState.Site.SiteId);
        Show ??= await LanguageService.ShowMenu(PageState.Site.SiteId);
    }

    public async Task SetLanguage(string culture) => await LanguageService.SetCultureAsync(culture);

    public string? Classes(MagicLanguage? lang, string tag) => (Designer as LanguagesDesigner)?.Classes(lang, tag).EmptyAsNull();
}