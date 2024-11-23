using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.UserLogins.Internal;

internal class MagicUserLoginKit : IMagicUserLoginKit
{
    public required MagicUser User { get; init; }

    public required MagicUserLoginResources Resources { get; init; }

    /// <summary>
    /// TODO: not final, probably not correct!
    /// </summary>
    public required MagicThemeDesigner Designer { get; init; }

    internal required OqtaneLoginHelperWip LoginHelper { get; init; }

    public async Task ToggleLogin() => await LoginHelper.ToggleLogin(User.PageState);
}