using Oqtane.UI;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.UserLogins.Internal;

namespace ToSic.Cre8magic.Users.Internal;

internal class MagicUserService(OqtaneLoginHelperWip loginHelper, IMagicSettingsService settingsSvc) : IMagicUserService
{
    public MagicUser User(PageState pageState) => new(pageState);
}