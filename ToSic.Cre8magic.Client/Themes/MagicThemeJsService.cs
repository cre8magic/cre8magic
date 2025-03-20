using Microsoft.JSInterop;
using ToSic.Cre8magic.Internal.JsInterops;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Themes;

/// <summary>
/// Constants and helpers related to JS calls from the Theme to it's own JS libraries
/// </summary>
// todo: find out if we can use ThemePath() somehow, inject?
public class MagicThemeJsService(IJSRuntime jsRuntime, IMagicSettingsService settingsSvc)
    : MagicJsServiceBase(jsRuntime, settingsSvc, $"_content/{MagicConstants.PackageId}/interop.js"), IMagicThemeJsService
{
    /// <inheritdoc />
    public async Task SetBodyClasses(string classes)
    {
        await InvokeAsync<string>("setBodyClass", classes);
    }
}