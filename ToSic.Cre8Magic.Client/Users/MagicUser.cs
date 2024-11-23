using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Users;

public record MagicUser
{
    /// <summary>
    /// Note: needs a custom constructor because the property PageState should be internal
    /// </summary>
    /// <param name="pageState"></param>
    public MagicUser(PageState pageState)
    {
        PageState = pageState;
    }

    internal readonly PageState PageState;

    public int Id => OqtaneUser?.UserId ?? 0;

    public string Username => OqtaneUser?.Username;

    public string Email => OqtaneUser?.Email;

    public string DisplayName => OqtaneUser?.DisplayName;

    public bool IsAnonymous => !IsAuthenticated;

    public bool IsAuthenticated => OqtaneUser is { IsAuthenticated: true };

    public bool IsEditor => PageState.UserIsEditor();

    public bool IsAdmin => PageState.UserIsAdmin();

    public bool IsRegistered => PageState.UserIsRegistered();

    public bool MayEditCurrentPage => IsEditor || (PageState.Page.IsPersonalizable && IsRegistered);

    public User? OqtaneUser => PageState.User;
}