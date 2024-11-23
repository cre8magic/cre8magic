using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Users.Internal;

namespace ToSic.Cre8magic.Users;

internal class MagicUserService(OqtaneLoginHelperWip loginHelper, IMagicSettingsService settingsSvc) : IMagicUserService
{
    public IMagicUserLoginKit UserKit(PageState pageState)
    {
        var user = User(pageState);
        var themeCtx = settingsSvc.GetThemeContextFull(pageState);

        return new MagicUserLoginKit
        {
            User = user,
            LoginHelper = loginHelper,
            Designer = new(themeCtx)
        };
    }

    public MagicUser User(PageState pageState) => new(pageState);
}