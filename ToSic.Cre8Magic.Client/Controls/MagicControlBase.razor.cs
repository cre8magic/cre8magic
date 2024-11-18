using Microsoft.AspNetCore.Components;
using Oqtane.Themes;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Controls;

/// <summary>
/// Oqtane Blazor Control with Settings
/// </summary>
public abstract class MagicControlBase: ThemeControlBase
{
    [Inject]
    public IMagicFactoryWip MagicFactory { get; set; }

    [CascadingParameter]
    public MagicAllSettings AllSettings { get; set; }

    protected bool UserIsAdmin => PageState.UserIsAdmin();

    protected bool UserIsEditor => PageState.UserIsEditor();

    protected bool UserIsLoggedIn => PageState.UserIsRegistered();

    protected virtual IMagicDesigner Designer => _designer ??= MagicFactory.ThemeDesigner(PageState);
    private IMagicDesigner? _designer;

}