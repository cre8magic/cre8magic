using Microsoft.AspNetCore.Components;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Base class for any menu list
/// </summary>
public abstract class MagicMenu : MagicMenuBase
{
    [Parameter]
    public required IMagicMenuKit MenuKit { get; set; }
}