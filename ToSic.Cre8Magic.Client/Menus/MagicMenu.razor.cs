using Microsoft.AspNetCore.Components;
using Oqtane.Themes;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Base class for any menu list
/// </summary>
public abstract class MagicMenu : ThemeControlBase
{
    [Parameter, EditorRequired]
    public required IMagicMenuKit MenuKit { get; set; }
}