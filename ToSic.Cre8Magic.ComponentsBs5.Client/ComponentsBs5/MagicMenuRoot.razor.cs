using Microsoft.AspNetCore.Components;
using Oqtane.Themes;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings;

// TODO:
// 1. Reduce to only use MagicMenuSettings / PartName etc.
// 2. Change to use base class without all the theme stuff
// 2. Then probably merge with MagicMenu?


namespace ToSic.Cre8magic.ComponentsBs5;

/// <summary>
/// Base class for Razor menus
/// </summary>
public abstract class MagicMenuRoot: ThemeControlBase
{
    /// <summary>
    /// Complex object with all settings.
    /// If this is used, all other settings will be ignored.
    /// </summary>
    [Parameter] public MagicMenuSettings? Settings { get; set; }

    [Inject] public required IMagicHat MagicHat { get; set; }

    protected IMagicMenuKit MenuKit => _menuKit ??= MagicHat.MenuKit(Settings.With(PageState));
    private IMagicMenuKit? _menuKit;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        // if parameters changed, reset menuKit
        _menuKit = null;
    }
    
}