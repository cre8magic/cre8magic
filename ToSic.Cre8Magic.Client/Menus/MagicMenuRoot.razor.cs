using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Oqtane.Themes.Controls;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Menus;

/// <summary>
/// Base class for Razor menus
/// </summary>
public abstract class MagicMenuRoot: MagicMenuBase
{
    /// <summary>
    /// Complex object with all settings.
    /// If this is used, all other settings will be ignored.
    /// </summary>
    [Parameter] public MagicMenuSettings? MenuSettings { get; set; }

    [Parameter] public bool? Debug { get; set; }


    #region Properties which should not be used any more - to be removed

    [Parameter] public string? MenuId { get; set; }
    [Parameter] public string? PartName { get; set; }
    /// <inheritdoc />
    [Parameter] public List<int>? PageList { get; set; }
    [Parameter] public bool? Children { get; set; }

    [Parameter] public int? Depth { get; set; }
    [Parameter] public bool? Display { get; set; } = true;
    [Parameter] public int? Level { get; set; }
    [Parameter] public string? Start { get; set; }
    [Parameter] public string? Design { get; set; }

    #endregion


    [Parameter] public string? Template { get; set; }

    protected IMagicPageList? Menu { get; private set; }

    [Inject] public ILogger<Menu>? Logger { get; set; }

    [Inject] public IMagicMenuService? MagicMenuService { get; set; }

    /// <summary>
    /// Detect if the menu is configured for vertical.
    /// For the most common 2 kinds of menu options. 
    /// </summary>
    protected bool IsVertical => MagicConstants.MenuVertical.EqInvariant(Menu?.Settings?.Variant);
    protected bool IsHorizontal => MagicConstants.MenuHorizontal.EqInvariant(Menu?.Settings?.Variant);

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var startSettings = MenuSettings;
        var menuSettings = MenuSettings ?? new MagicMenuSettings
        {
            Id = MenuId ?? startSettings?.MenuId,
            Debug = Debug == null
                ? startSettings?.Debug
                : new() { Allowed = Debug, Admin = Debug, Anonymous = Debug },
            Template = Template ?? startSettings?.Template,
            Children = Children ?? startSettings?.Children,
            PartName = PartName ?? startSettings?.PartName,
            Depth = Depth ?? startSettings?.Depth,
            Display = Display ?? startSettings?.Display,
            Level = Level ?? startSettings?.Level,
            Start = Start ?? startSettings?.Start
        };

        Menu = MagicMenuService?.GetMenu(PageState, menuSettings);
    }

}