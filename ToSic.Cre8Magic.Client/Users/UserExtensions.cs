using Oqtane.Security;
using Oqtane.Shared;
using Oqtane.UI;

namespace ToSic.Cre8magic.Users;

public static class UserExtensions
{
    /// <summary>
    /// Quickly ask the PageState if the user is allowed to edit the current page.
    /// Typically used to decide if certain buttons or information should be shown.
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    public static bool UserMayEditCurrentPage(this PageState pageState)
        => pageState.User != null && UserSecurity.IsAuthorized(pageState.User, PermissionNames.Edit, pageState.Page.PermissionList);

    
    internal static bool UserIsAdmin(this PageState pageState)
        => pageState.User != null && UserSecurity.IsAuthorized(pageState.User, PermissionNames.Edit, pageState.Page.PermissionList);

    internal static bool UserIsRegistered(this PageState pageState)
        => pageState.User != null && UserSecurity.IsAuthorized(pageState.User, RoleNames.Registered);


}