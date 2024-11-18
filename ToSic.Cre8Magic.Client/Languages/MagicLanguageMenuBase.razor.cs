using Microsoft.AspNetCore.Components;

namespace ToSic.Cre8magic.Languages;

public abstract class MagicLanguagesMenuBase: MagicControlBase
{
    [Inject]
    protected MagicLanguagesService LanguageService { get; set; }

    public MagicLanguagesState? State { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        // Load defined language list. It changes unless the page is reloaded, so we can cache it on this control
        State ??= await LanguageService.State(PageState);
    }
}