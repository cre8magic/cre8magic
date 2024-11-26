using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Containers;

namespace ToSic.Cre8magic.ComponentsBs5;

public partial class MagicContainer: Oqtane.Themes.ContainerBase
{
    /// <summary>
    /// Visible name in the UI (unless overridden again in the inheriting container)
    /// </summary>
    public override string Name => "Default (for Content and Admin)";

    public virtual MagicContainerSettings? Settings => null;

    [Inject]
    public required IMagicHat MagicHat { get; set; }

    #region Navigation / Close

    public string CloseUrl { get; private set; } = "#";

    /// <summary>
    /// This should update the URL.
    /// But it only works on the "first" popup-dialog, anything after that will have a real url in the path and will
    /// actually just point to the same page.
    ///
    /// TODO: figure out a better way to remember the way back...
    /// </summary>
    protected override void OnParametersSet() => 
        CloseUrl = !string.IsNullOrEmpty(PageState.ReturnUrl) ? PageState.ReturnUrl : NavigateUrl();

    #endregion

    private IMagicContainerKit ContainerKit => _kit ??= MagicHat.ContainerKit(PageState, ModuleState);
    private IMagicContainerKit? _kit;

}