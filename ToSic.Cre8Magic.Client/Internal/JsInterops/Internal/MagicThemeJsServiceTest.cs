using Microsoft.JSInterop;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Internal.JsInterops.Internal;

/// <summary>
/// Constants and helpers related to JS calls from the Theme to its own JS libraries
/// </summary>
public class MagicThemeJsServiceTest(IJSRuntime jsRuntime, IMagicSettingsService settingsSvc)
    : MagicJsServiceBase(jsRuntime, settingsSvc, $"./_content/{MagicConstants.PackageId}/test.js"), IMagicThemeJsService
{
    /// <inheritdoc />
    public async Task ClearBodyClasses() 
        => await InvokeVoidAsync("clearBodyClasses");

    /// <inheritdoc />
    public async Task SetBodyClasses(string classes)
        => await InvokeVoidAsync("setBodyClass", classes);

    ///// <inheritdoc />
    //public async Task GtmActivate(string gtmId)
    //    => await InvokeVoidAsync("gtmActivate", gtmId);

    ///// <inheritdoc />
    //public async Task GtmPageView()
    //    => await InvokeVoidAsync("gtmPageView");

    public async Task<string> TestFromTest()
        => await InvokeAsync<string>("testFromTest");
}
