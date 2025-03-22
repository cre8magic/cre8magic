using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Themes;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.OqtaneBasic;

/// <summary>
/// Base class for our themes. It's responsible for
///
/// 1. Some basic properties such as Name, BodyClasses etc. which each theme can configure
/// 2. Adding special classes to the body tag so that the CSS can best optimize for each scenario
/// </summary>
/// <remarks>
/// - The base class must be abstract, so that Oqtane doesn't see it as a real them.
/// - The config-properties must be abstract, so the inheriting files are forced to set them. 
/// </remarks>
public abstract class MagicTheme : Oqtane.Themes.ThemeBase
{
    /// <summary>
    /// This contains the default settings which must be used in this theme.
    /// Any inheriting class must specify what it will be. 
    /// </summary>
    public abstract MagicThemePackage ThemePackage { get; }

    /// <summary>
    /// The Theme Kit which is help the theme become awesome.
    /// </summary>
    public IMagicThemeKit ThemeKit => _themeKit.Get(PageState, () => MagicAct.ThemeKit(new() { PageState = PageState }));
    private readonly CacheByPage<IMagicThemeKit> _themeKit = new();

    /// <summary>
    /// The <see cref="MagicAct"/> which coordinates everything.
    /// </summary>
    /// <remarks>
    /// It's provided by Dependency Injection, required.
    /// </remarks>
    [Inject]
    public required IMagicAct MagicAct
    {
        get => _magicAct;
        [MemberNotNull(nameof(_magicAct))]
        set => _magicAct = value.UseThemePackage(ThemePackage);
    }
    private IMagicAct _magicAct;

    /// <summary>
    /// Handle OnParametersSet to provide the latest PageState to the MagicAct.
    /// </summary>
    /// <remarks>
    /// OnParametersSet runs whenever any parameter changes - such as PageState.
    /// It also runs before OnParametersSetAsync.
    /// </remarks>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        // Provide the latest PageState on every change
        MagicAct.UsePageState(PageState);
    }


    /// <summary>
    /// Handle OnAfterRenderAsync to track page views
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        await base.OnAfterRenderAsync(isFirstRender);
        // Provide the latest PageState on every change,
        // but OnAfterRenderAsync could be executed without OnParametersSet
        MagicAct.UsePageState(PageState);
        await MagicAct.AnalyticsKit().TrackPage(isFirstRender);
    }
}