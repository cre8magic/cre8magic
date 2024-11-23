using Oqtane.UI;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.UserLogins.Internal;

public interface IUserLoginService
{
    IMagicUserLoginKit UserKit(PageState pageState);
}