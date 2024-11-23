using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic.Users;

public interface IMagicUserLoginKit
{
    MagicUser User { get; init; }

    /// <summary>
    /// TODO: not final, probably not correct!
    /// </summary>
    MagicThemeDesigner Designer { get; init; }

    Task ToggleLogin();
}