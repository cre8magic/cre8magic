using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.JsInterop.Internal;

/// <summary>
/// Base for any JS Module Helper class for Custom JS Services on Themes. 
/// It will take care of loading the module and provide basic helpers to call functions etc.
/// </summary>
/// <param name="jsRuntime">JS Runtime of the control, usually available later, like in the OnAfterRenderAsync</param>
/// <param name="settingsSvc"></param>
/// <param name="modulePath">Path to the javascript file, must be a JS6 Module</param>
public abstract class MagicCustomJsServiceBase(IJSRuntime jsRuntime, IMagicSettingsService settingsSvc, string modulePath)
    : MagicJsServiceBase(jsRuntime, modulePath)
{
    protected const string ThemePathPlaceholder = "[ThemePath]";

    /// <summary>
    /// The path to the javascript module.
    /// Note that it would also replace the `[ThemePath]` Placeholder with the actual theme path.
    /// </summary>
    [field: AllowNull, MaybeNull]
    protected override string ModulePath => field ??= modulePath?.Replace(ThemePathPlaceholder, settingsSvc.ThemePackage.Url) ?? "";
}