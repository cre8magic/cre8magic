using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Users;

public record MagicUser
{
    public MagicUser(PageState pageState)
    {
        _pageState = pageState;
        OqtaneUser = pageState.User;
    }
    private readonly PageState _pageState;

    public int Id => OqtaneUser.UserId;

    public string Username => OqtaneUser.Username;

    public string Email => OqtaneUser.Email;

    public string DisplayName => OqtaneUser.DisplayName;

    public bool IsAnonymous => !OqtaneUser.IsAuthenticated;
    public bool IsAuthenticated => OqtaneUser.IsAuthenticated;

    public bool IsEditor => _pageState.UserIsEditor();

    public bool IsAdmin => _pageState.UserIsAdmin();

    public bool IsRegistered => _pageState.UserIsRegistered();

    public bool MayEditCurrentPage => IsEditor || (_pageState.Page.IsPersonalizable && IsRegistered);

    public User OqtaneUser { get; }
}