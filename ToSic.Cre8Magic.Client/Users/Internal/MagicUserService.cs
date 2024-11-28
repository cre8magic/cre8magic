using Oqtane.UI;

namespace ToSic.Cre8magic.Users.Internal;

internal class MagicUserService : IMagicUserService
{
    public MagicUser User(PageState pageState) => new(pageState);
}