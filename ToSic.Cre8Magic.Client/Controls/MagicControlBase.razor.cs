using Microsoft.AspNetCore.Components;
using Oqtane.Themes;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Controls;

/// <summary>
/// Oqtane Blazor Control with Settings
/// </summary>
public abstract class MagicControlBase: ThemeControlBase, IMagicControlWithSettings
{
    [CascadingParameter] public MagicAllSettings AllSettings { get; set; }

    protected MagicPageFactory PageFactory => _pageFactory.Get(() => new(PageState), f => f?.PageState.Page == PageState.Page);
    private readonly GetKeep<MagicPageFactory> _pageFactory = new();

    protected bool UserIsAdmin => PageState.UserIsAdmin();

    protected bool UserIsEditor => PageState.UserIsEditor();

    protected bool UserIsLoggedIn => PageState.UserIsRegistered();

    protected virtual IMagicDesigner Designer => _designer ??= AllSettings?.ThemeDesigner;
    private IMagicDesigner? _designer;

    public string? Classes(string target) => Designer.Classes(target);

    public string? ClassesOrDefault(string target, string defaultValue) => Classes(target) ?? defaultValue;

    public string? Value(string target) => Designer.Value(target);

    public string? Id(string name) => Designer.Id(name);
}