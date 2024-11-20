using Microsoft.AspNetCore.Components;
using Oqtane.UI;

namespace ToSic.Cre8magic.Components;

/// <summary>
/// Plain Vanilla Blazor component with PageState as base for your controls.
///
/// We highly recommend you use this instead of the `OqtaneControlBase` classes,
/// because they are quite heavy and don't adhere to Composition over Inheritance paradigms.
/// </summary>
public abstract class MagicComponentBase: ComponentBase
{
    /// <summary>
    /// The PageState, as will be injected automatically by Blazor when the component is initialized.
    ///
    /// In most cases, this is all you need to do everything you want.
    /// </summary>
    [CascadingParameter]
    public required PageState PageState { get; set; }
}