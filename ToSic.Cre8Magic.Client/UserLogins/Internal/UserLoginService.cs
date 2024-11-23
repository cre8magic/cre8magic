using Microsoft.Extensions.Localization;
using Oqtane;
using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.UserLogins.Internal;

internal class UserLoginService(OqtaneLoginHelperWip loginHelper, IMagicSettingsService settingsSvc, IStringLocalizer<SharedResources> localizer) : IUserLoginService
{
    public IMagicUserLoginKit UserLoginKit(PageState pageState)
    {
        var user = new MagicUser(pageState);
        var themeCtx = settingsSvc.GetThemeContextFull(pageState);

        
        return new MagicUserLoginKit
        {
            User = user,
            LoginHelper = loginHelper,
            Designer = new(themeCtx),
            Resources = new(localizer, user),
        };
    }
}