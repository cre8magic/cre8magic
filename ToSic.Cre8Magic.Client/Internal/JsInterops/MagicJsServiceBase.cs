using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Internal.JsInterops;

/// <summary>
/// Base for any JS Module Helper class
/// </summary>
public abstract class MagicJsServiceBase
{
    protected const string ThemePathPlaceholder = "[ThemePath]";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="jsRuntime">JS Runtime of the control, usually available later, like in the OnAfterRenderAsync</param>
    /// <param name="modulePath">Path to the javascript file, must be a JS6 Module</param>
    protected MagicJsServiceBase(IJSRuntime jsRuntime, IMagicSpellsService spellsSvc, string modulePath)
    {
        _spellsSvc = spellsSvc;
        JsRuntime = jsRuntime;
        // ModulePath = modulePath;
        ModulePath = modulePath;
    }
    private readonly IMagicSpellsService _spellsSvc;

    protected IJSRuntime JsRuntime { get; }

    [field: AllowNull, MaybeNull]
    protected string ModulePath => field?.Replace(ThemePathPlaceholder, _spellsSvc.ThemePackage.ThemePath);

    public async Task Log(params object[] args) => await JsRuntime.InvokeVoidAsync("console.log", args);

    /// <summary>
    /// The JsObjectReference to the real module.
    /// Will need to load it on first access, so it's async. 
    /// </summary>
    /// <returns></returns>
    public async Task<IJSObjectReference> Module() => _jsModule
        ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", ModulePath);
    private IJSObjectReference? _jsModule;

    protected async Task<TValue> InvokeAsync<TValue>(string identifier)
        => await (await Module()).InvokeAsync<TValue>(identifier);

    protected async Task<TValue> InvokeAsync<TValue>(string identifier, params object[] args)
        => await (await Module()).InvokeAsync<TValue>(identifier, args);
}