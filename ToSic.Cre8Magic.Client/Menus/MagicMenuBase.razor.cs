using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils;

// TODO: adapt Cre8magic everywhere
namespace ToSic.Cre8magic.Client.Menus;

public abstract class MagicMenuBase: Oqtane.Themes.Controls.MenuBase, IMagicControlWithSettings // TODO: stv use ThemeControlBase instead MenuBase and use MenuPageService that replaces MenuBase
{
    [CascadingParameter]
    public MagicAllSettings AllSettings { get; set; }

    protected MagicPageFactory PageFactory => _pageFactory.Get(() => new(PageState), f => f?.PageState.Page == PageState.Page);
    private readonly GetKeep<MagicPageFactory> _pageFactory = new();

    private const string ErrMsg = "error calling {0} in {1}. Use the {0} method of the branch to get the expected result.";

    public string? Classes(string target) => string.Format(ErrMsg, nameof(Classes), nameof(MagicMenuBase));
    public string? Value(string target) => string.Format(ErrMsg, nameof(Value), nameof(MagicMenuBase));

    public string? Id(string target) => string.Format(ErrMsg, nameof(Id), nameof(MagicMenuBase));
}