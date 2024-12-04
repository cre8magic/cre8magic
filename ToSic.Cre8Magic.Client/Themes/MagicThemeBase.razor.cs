using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes;

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
public abstract class MagicThemeBase : Oqtane.Themes.ThemeBase
{
    /// <summary>
    /// Name to show in the Theme-picker.
    /// Must be set by each inheriting theme, which is why it's marked abstract to enforce this.
    /// </summary>
    public abstract override string Name { get; }

    /// <summary>
    /// This contains the default settings which must be used in this theme.
    /// Any inheriting class must specify what it will be. 
    /// </summary>
    public abstract MagicThemePackage ThemePackage { get; }


    public IMagicThemeKit ThemeKit => _themeKit.Get(PageState, () => MagicAct.ThemeKit(new() { PageState = PageState }));
    private readonly GetKeepByPageId<IMagicThemeKit> _themeKit = new();

    /// <summary>
    /// Get the <see cref="MagicAct"/> from the DI
    /// </summary>
    [Inject] public required IMagicAct MagicAct { get; set; }

    /// <summary>
    /// OnInitialized will run early (and once only).
    /// It also runs before OnInitializedAsync.
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        // Provide the first PageState as early as possible.
        MagicAct.UsePageState(PageState);
        MagicAct.UseThemePackage(ThemePackage);
    }

    /// <summary>
    /// This will run whenever any parameter changes - such as PageState.
    /// It also runs before OnParametersSetAsync.
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        // Provide the latest PageState on every change
        MagicAct.UsePageState(PageState);
    }


    /// <summary>
    /// OnAfterRender, track page views
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        await MagicAct.AnalyticsKit().TrackPage(firstRender);
    }
}