using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Languages.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

public abstract class MagicLanguageMenuBase: MagicControlBase
{

    [Inject] protected LanguageService LanguageService { get; set; }

    public List<MagicLanguage> Languages { get; private set; }

    protected override IMagicDesigner Designer => _designer ??= MagicFactory.LanguagesDesigner(PageState);
    private IMagicDesigner? _designer;

    /// <summary>
    /// Determines if the languages should be shown. Will be retrieved from the settings
    /// </summary>
    protected bool? Show { get; private set; } = null;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        // Load defined language list. It changes unless the page is reloaded, so we can cache it on this control
        Languages ??= await LanguageService.LanguagesToShow(PageState);
        Show ??= await LanguageService.ShowMenu(PageState);
    }

    public async Task SetLanguage(string culture) =>
        await LanguageService.SetCultureAsync(culture);

    public string? Classes(MagicLanguage? lang, string tag) =>
        (Designer as LanguagesDesigner)?.Classes(lang, tag).EmptyAsNull();
}