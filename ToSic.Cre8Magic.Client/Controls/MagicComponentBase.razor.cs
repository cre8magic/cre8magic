using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Controls;

/// <summary>
/// Non-Oqtane Blazor component with Settings as base for your controls
/// </summary>
public abstract class MagicComponentBase: ComponentBase, IMagicControlWithSettings
{
    [CascadingParameter] public MagicAllSettings AllSettings { get; set; }

    public string? Classes(string target) => AllSettings.ThemeDesigner.Classes(target);

    public string? Value(string target) => AllSettings.ThemeDesigner.Value(target);

    public string? Id(string name) => AllSettings.ThemeDesigner.Id(name);
}