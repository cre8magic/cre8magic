using Microsoft.Extensions.Localization;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.UserLogins;

public record MagicUserLoginResources
{
    internal MagicUserLoginResources(IStringLocalizer localizer, MagicUser user)
    {
        _localizer = localizer;
        _user = user;
    }

    private readonly IStringLocalizer _localizer;
    private readonly MagicUser _user;

    public string Title => _user.IsAuthenticated ? Logout : Login;

    public string Login => Localize("Login");

    public string Logout => Localize("Logout");

    //public string Register => Localize("Register");

    //public string ForgotPassword => Localize("ForgotPassword");

    //public string ResetPassword => Localize("ResetPassword");

    //public string ChangePassword => Localize("ChangePassword");



    private string Localize(string key) => _localizer[key];
}