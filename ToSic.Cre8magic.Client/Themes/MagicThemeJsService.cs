using Microsoft.JSInterop;
using ToSic.Cre8magic.JsInterop.Internal;

namespace ToSic.Cre8magic.Themes;

/// <summary>
/// Constants and helpers related to JS calls from the Theme to its own JS libraries
/// </summary>
public class MagicThemeJsService(IJSRuntime jsRuntime)
    : MagicJsServiceBase(jsRuntime, MagicConstants.InteropJsPath), IMagicThemeJsService
{
    /// <inheritdoc />
    public async Task ClearBodyClasses()
        => await InvokeVoidAsync("clearBodyClasses");

    /// <inheritdoc />
    public async Task SetBodyClasses(string classes)
        => await InvokeVoidAsync("setBodyClass", classes);

}