using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;

namespace ToSic.Cre8magic.Utils;

public static class PageStateSecurityExtensions
{
    /// <summary>
    /// Modules are treated as admin modules (and must use the admin container) if they are marked as such, or come from the Oqtane ....Admin... type
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    internal static bool ForceAdminContainer(this Module module) 
        => module.UseAdminContainer || module.ModuleType.Contains(".Admin.");

    internal static bool IsPublished(this Module module)
        => UserSecurity.ContainsRole(module.PermissionList, PermissionNames.View, RoleNames.Everyone);

}