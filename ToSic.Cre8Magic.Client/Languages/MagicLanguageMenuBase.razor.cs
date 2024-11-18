using Microsoft.AspNetCore.Components;

namespace ToSic.Cre8magic.Languages;

public abstract class MagicLanguageMenuBase: MagicControlBase
{
    [Inject]
    protected IMagicLanguageService LanguageService { get; set; }

    public MagicLanguageState? State { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        // Load defined language list. It changes unless the page is reloaded, so we can cache it on this control
        State ??= await LanguageService.GetStateAsync(PageState);
    }
}