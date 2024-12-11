using Oqtane.UI;

namespace ToSic.Cre8magic.UserLogins.Internal;

public interface IUserLoginService
{
    IMagicUserLoginKit UserLoginKit(PageState pageState, MagicUserLoginSpell? settings);
}