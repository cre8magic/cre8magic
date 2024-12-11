using Microsoft.Extensions.Localization;
using Oqtane;
using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.UserLogins.Internal;

internal class UserLoginService(OqtaneLoginHelperWip loginHelper, IMagicSpellsService spellsSvc, IStringLocalizer<SharedResources> localizer) : IUserLoginService
{
    public IMagicUserLoginKit UserLoginKit(PageState pageState, MagicUserLoginSpell? settings)
    {
        // Note: ATM the settings are not yet used, as there is no value stored in them
        // Setup is just for API consistency and future use

        var user = new MagicUser(pageState);
        var themeCtx = spellsSvc.GetThemeContextFull(pageState);

        return new MagicUserLoginKit
        {
            User = user,
            LoginHelper = loginHelper,
            Tailor = new(themeCtx),
            Resources = new(localizer, user),
        };
    }
}