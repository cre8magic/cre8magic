using Microsoft.AspNetCore.Components;
using Oqtane.UI;

namespace ToSic.Cre8magic.Components;

/// <summary>
/// Non-Oqtane Blazor component with Settings as base for your controls
/// </summary>
public abstract class MagicComponentBase: ComponentBase
{
    [CascadingParameter]
    public required PageState PageState { get; set; }

}