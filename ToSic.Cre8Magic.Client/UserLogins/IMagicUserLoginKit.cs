using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Users;

namespace ToSic.Cre8magic.UserLogins;

public interface IMagicUserLoginKit
{
    MagicUser User { get; init; }

    /// <summary>
    /// TODO: not final, probably not correct!
    /// </summary>
    MagicThemeTailor Tailor { get; init; }

    MagicUserLoginResources Resources { get; init; }

    Task ToggleLogin();
}