using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace ToSic.Cre8magic.JsInterop.Internal;

/// <summary>
/// Base for any JS Module Helper class.
/// It will take care of loading the module and provide basic helpers to call functions etc.
/// </summary>
/// <param name="jsRuntime">JS Runtime of the control, usually available later, like in the OnAfterRenderAsync</param>
/// <param name="modulePath">Path to the javascript file, must be a JS6 Module</param>
public abstract class MagicJsServiceBase(IJSRuntime jsRuntime, string modulePath)
{
    protected IJSRuntime JsRuntime { get; } = jsRuntime;

    /// <summary>
    /// The path to the javascript module.
    /// Note that it would also replace the `[ThemePath]` Placeholder with the actual theme path.
    /// </summary>
    [field: AllowNull, MaybeNull]
    protected virtual string ModulePath => field ??= modulePath ?? "";

    public async Task Log(params object[] args) =>
        await JsRuntime.InvokeVoidAsync("console.log", args);

    /// <summary>
    /// The JsObjectReference to the real module.
    /// Will need to load it on first access, so it's async. 
    /// </summary>
    /// <returns></returns>
    public async Task<IJSObjectReference> Module() =>
        _jsModule ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", ModulePath);
    private IJSObjectReference? _jsModule;

    protected async Task<TResult> InvokeAsync<TResult>(string identifier)
        => await (await Module()).InvokeAsync<TResult>(identifier);

    protected async Task<TResult> InvokeAsync<TResult>(string identifier, params object[] args)
        => await (await Module()).InvokeAsync<TResult>(identifier, args);

    protected async Task InvokeVoidAsync(string identifier)
        => await (await Module()).InvokeVoidAsync(identifier);

    protected async Task InvokeVoidAsync(string identifier, params object[] args)
        => await (await Module()).InvokeVoidAsync(identifier, args);
}