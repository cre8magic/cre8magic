using System.Text.Json.Serialization;
using Oqtane.UI;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// All the current "global" settings of a page, which apply to anything on the page.
/// </summary>
public record MagicAllSettings: IHasDebugSettings
{
    internal MagicAllSettings(string name, IMagicSettingsService service, MagicThemeSettings themeSettings, MagicThemeDesigner designer, TokenEngine tokens, PageState pageState)
    {
        //Name = name;
        //Service = service;
        ThemeSettings = themeSettings;
        //ThemeDesigner = designer;
        //Tokens = tokens;
        PageState = pageState;
    }

    //public MagicDebugState Debug => _debug ??= Service.Debug.GetState(ThemeSettings, PageState.UserIsAdmin());
    //private MagicDebugState? _debug;

    /// <summary>
    /// This is only used to detect if debugging should be active, and the setting should come from the theme itself
    /// </summary>
    MagicDebugSettings? IHasDebugSettings.Debug => ThemeSettings.Debug;

    internal PageState PageState { get; }

    //internal TokenEngine Tokens { get; }

    //public string Name { get; }

    //[JsonIgnore]
    //public IMagicSettingsService Service { get; }

    //[JsonIgnore]
    //internal MagicThemeDesigner ThemeDesigner { get; init; }

    public MagicThemeSettings ThemeSettings { get; }

    ///// <summary>
    ///// Determine if we should show a specific part
    ///// </summary>
    //public bool ShowPart(string name) =>
    //    ThemeSettings.Parts.TryGetValue(name, out var value) && value.Show == true;
}