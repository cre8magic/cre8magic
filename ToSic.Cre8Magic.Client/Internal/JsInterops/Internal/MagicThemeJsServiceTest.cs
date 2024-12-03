using Microsoft.JSInterop;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Internal.JsInterops.Internal;

/// <summary>
/// Constants and helpers related to JS calls from the Theme to its own JS libraries
/// </summary>
// TODO: SOME DAY move to Cre8magic, as soon as we know how to reliably include the js-assets in the final distribution
public class MagicThemeJsServiceTest(IJSRuntime jsRuntime, IMagicSettingsService settingSvc)
    : MagicJsServiceBase(jsRuntime, settingSvc, $"./_content/{MagicConstants.PackageId}/test.js"), IMagicThemeJsService
{
    /// <inheritdoc />
    public async Task SetBodyClasses(string classes) 
    {
        await InvokeAsync<string>("setBodyClass", classes);
    }

    public async Task<string> TestFromTest()
    {
        return await InvokeAsync<string>("testFromTest");
    }
}