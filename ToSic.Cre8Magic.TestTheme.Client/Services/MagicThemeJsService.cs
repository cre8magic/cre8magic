using System.Threading.Tasks;
using Microsoft.JSInterop;
using ToSic.Cre8magic.JsModules;

namespace ToSic.Cre8magic.TestTheme.Client.Services;

/// <summary>
/// Constants and helpers related to JS calls from the Theme to it's own JS libraries
/// </summary>
// TODO: SOME DAY move to Cre8magic, as soon as we know how to reliably include the js-assets in the final distribution
// todo: find out if we can use ThemePath() somehow, inject?
public class MagicThemeJsService(IJSRuntime jsRuntime)
    : MagicJsServiceBase(jsRuntime, $"./{ThemeInfo.ThemePath}/interop/page-control.js"), IMagicThemeJsService
{
    /// <inheritdoc />
    public async Task SetBodyClasses(string classes)
    {
        await InvokeAsync<string>("setBodyClass", classes);
    }
}