using Microsoft.AspNetCore.Components;
using Oqtane.Themes;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Controls;

/// <summary>
/// Oqtane Blazor Control with Settings
/// </summary>
public abstract class MagicControlBase: ThemeControlBase
{
    [CascadingParameter]
    public MagicAllSettings AllSettings { get; set; }
}