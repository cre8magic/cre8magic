using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Oqtane;
using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic.Login;

public abstract class MagicLoginBase: Oqtane.Themes.Controls.LoginBase
{
    [Inject]
    private IStringLocalizer<SharedResources> Localizer { get; set; }

    [Inject]
    public IMagicFactoryWip MagicFactory { get; set; }

    private MagicThemeDesigner Designer => _designer ??= MagicFactory.ThemeDesigner(PageState);
    private MagicThemeDesigner? _designer;


    protected bool IsLoggedIn => _isLoggedIn ??= PageState.User is { IsAuthenticated: true };
    private bool? _isLoggedIn;

    protected string LocalizedLabel => Localizer[IsLoggedIn ? "Logout" : "Login"];

    protected async Task ChangeLogin()
    {
        if (IsLoggedIn)
            await LogoutUser();
        else
            LoginUser();
    }

    public string? Classes(string target) => Designer.Classes(target);

    public string? Value(string target) => Designer.Value(target);

    public string? Id(string name) => Designer.Id(name);
}