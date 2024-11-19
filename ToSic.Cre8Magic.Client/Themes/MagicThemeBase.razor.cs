using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Components;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;
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
    /// The layout name which is used to lookup configurations.
    /// The inheriting file is required to specify it. 
    /// </summary>
    public abstract string Layout { get; }

    /// <summary>
    /// Sets additional body classes - usually to activate CSS variations for this theme
    /// 
    /// Note that ATM they are not set on the body, but on a div wrapping the entire contents.
    /// This is because of some shortcomings in Oqtane, which doesn't allow to reliably set classes on the body.
    /// Just remember that when you write your CSS ;).
    /// </summary>
    protected abstract string MagicContextClasses { get; }

    /// <summary>
    /// Option to inject dynamic components - mainly for testing
    /// inspired by http://www.binaryintellect.net/articles/a92dea29-3218-4d1c-a132-9671b518d1f4.aspx
    /// </summary>
    protected List<MagicDynamicComponent> MagicComponents { get; } = [];

    // Panes of the layout
    public const string PaneNameHeader = "Header";

    /// <summary>
    /// Force the user to overwrite panes.
    /// </summary>
    public abstract override string Panes { get; }

    /// <summary>
    /// Make a nicer theme path without the ".Client"
    /// </summary>
    /// <returns></returns>
    public new string ThemePath() => base.ThemePath().Replace(".Client", "");

    [Inject]
    protected IMagicSettingsService MagicSettingsService
    {
        get => _magicSettingsService!;
        set => _magicSettingsService = value.Setup(ThemePackageSettings, Layout);    // Init when injecting
    }
    private IMagicSettingsService? _magicSettingsService;
    
    /// <summary>
    /// This contains the default settings which must be used in this theme.
    /// Any inheriting class must specify what it will be. 
    /// </summary>
    public abstract MagicPackageSettings ThemePackageSettings { get; }

    [Inject]
    public IMagicAnalyticsService? MagicAnalytics { get; set; }

    [Inject]
    public IMagicThemeService? ThemeService { get; set; }

    public MagicThemeState ThemeState => _themeState.Get(PageState, () => ThemeService!.State(PageState));
    private readonly GetKeepByPageId<MagicThemeState> _themeState = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        // Track page views
        if (MagicAnalytics != null)
            await MagicAnalytics.TrackPage(PageState, firstRender);
    }

    public MagicThemeDesigner Designer => ThemeState.Designer ?? throw new("No settings available");
}