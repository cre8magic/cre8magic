﻿using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;

namespace ToSic.Cre8magic.Internal;

internal static class PageStateSecurityExtensions
{
    /// <summary>
    /// Modules are treated as admin modules (and must use the admin container) if they are marked as such, or come from the Oqtane ....Admin... type
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    /// <remarks>
    /// * 2025-03-24 Originally also checked if the module namespace contained ".Admin." but this was removed.
    /// * reason was that the toggle actually meant to check if the layout should use a popup, and all following dialogs would fail if popup because the "x" close button would not work.
    /// </remarks>
    internal static bool ForceAdminContainer(this Module module) 
        => module.UseAdminContainer; // || module.ModuleType.Contains(".Admin.");

    internal static bool IsPublished(this Module module)
        => UserSecurity.ContainsRole(module.PermissionList, PermissionNames.View, RoleNames.Everyone);

}