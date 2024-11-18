using Oqtane.UI;

namespace ToSic.Cre8magic.Users;

internal class MagicUserService : IMagicUserService
{
    public MagicUser User(PageState pageState)
    {
        return new MagicUser(pageState);
    }
}