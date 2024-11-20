using Microsoft.AspNetCore.Components;
using Oqtane.UI;

namespace ToSic.Cre8magic.Languages;

public abstract class MagicLanguageMenuBase: ComponentBase
{
    /// <inheritdoc cref="ToSic.Cre8magic.Components.MagicComponentBase.PageState"/>
    [CascadingParameter]
    public required PageState PageState { get; set; }

    [Inject]
    protected IMagicLanguageService LanguageService { get; set; }

    public IMagicLanguageKit? LanguageKit { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        // Load defined language list. It changes unless the page is reloaded, so we can cache it on this control
        LanguageKit ??= await LanguageService.LanguageKitAsync(PageState);
    }
}