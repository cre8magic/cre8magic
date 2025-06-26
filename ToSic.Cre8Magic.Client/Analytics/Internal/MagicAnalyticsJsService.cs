using Microsoft.JSInterop;
using ToSic.Cre8magic.JsInterop.Internal;

namespace ToSic.Cre8magic.Analytics.Internal;

/// <summary>
/// Constants and helpers related to JS calls from the Theme to its own JS libraries
/// </summary>
public class MagicAnalyticsJsService(IJSRuntime jsRuntime)
    : MagicJsServiceBase(jsRuntime, MagicConstants.InteropJsPath), IMagicAnalyticsJsService
{
    /// <inheritdoc />
    public async Task GtmAddToPage(string gtmId)
        => await InvokeVoidAsync("gtm.addToPage", gtmId);

    /// <inheritdoc />
    public async Task GtmPageView(string verb)
        => await InvokeVoidAsync("gtm.pageView", verb);

    /// <inheritdoc />
    public async Task Gtag(string target, string more)
        => await InvokeVoidAsync("gtm.gtag", target, more);
}