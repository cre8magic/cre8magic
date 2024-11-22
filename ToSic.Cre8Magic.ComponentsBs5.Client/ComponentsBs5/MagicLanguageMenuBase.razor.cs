using Microsoft.AspNetCore.Components;
using Oqtane.UI;
using ToSic.Cre8magic.ComponentsBs5.Internal;
using ToSic.Cre8magic.Languages;

namespace ToSic.Cre8magic.ComponentsBs5;

public abstract class MagicLanguageMenuBase: ComponentBase
{
    /// <inheritdoc cref="ComponentDocs.PageState"/>
    [CascadingParameter]
    public required PageState PageState { get; set; }

    [Inject]
    public required IMagicHat MagicHat { get; set; }

    public IMagicLanguageKit? LanguageKit { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        // Load defined language list. It changes unless the page is reloaded, so we can cache it on this control
        LanguageKit ??= await MagicHat.LanguageKitAsync(PageState);
    }
}