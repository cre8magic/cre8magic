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

    /// <summary>
    /// The User ID as specified in the DB.
    /// </summary>
    /// <returns>The ID or `0` (zero) if user is anonymous.</returns>
    public int Id => OqtaneUser?.UserId ?? 0;

    /// <summary>
    /// The username of the user.
    /// </summary>
    /// <returns>The username or null if the user is anonymous.</returns>
    public string? Username => OqtaneUser?.Username;

    /// <summary>
    /// The email address of the user.
    /// </summary>
    /// <returns>The email address or null if the user is anonymous.</returns>
    public string Email => OqtaneUser?.Email;

    /// <summary>
    /// The display name of the user.
    /// </summary>
    /// <returns>The display name or null if the user is anonymous.</returns>
    public string DisplayName => OqtaneUser?.DisplayName;

    /// <summary>
    /// Is the user anonymous (not authenticated)?
    /// </summary>
    public bool IsAnonymous => !IsAuthenticated;

    /// <summary>
    /// Is the user authenticated (not anonymous)?
    /// </summary>
    public bool IsAuthenticated => OqtaneUser is { IsAuthenticated: true };

    //private bool IsEditor => PageState.UserIsEditor();

    public bool MayAdminCurrentPage => PageState.UserIsAdmin();

    public bool IsRegistered => PageState.UserIsRegistered();

    public bool MayEditCurrentPage => PageState.UserIsEditor() || (PageState.Page.IsPersonalizable && IsRegistered);

    /// <summary>
    /// The underlying Oqtane User object.
    /// </summary>
    /// <returns>The object or null if the user is not authenticated.</returns>
    public User? OqtaneUser => PageState.User;
}