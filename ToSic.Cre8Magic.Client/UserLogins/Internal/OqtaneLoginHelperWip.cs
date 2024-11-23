using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Oqtane.Enums;
using Oqtane.Providers;
using Oqtane.Security;
using Oqtane.Services;
using Oqtane.Shared;
using Oqtane.UI;
using ToSic.Cre8magic.Internal;

namespace ToSic.Cre8magic.UserLogins.Internal;

/// <summary>
/// Special helper containing copied code from the LoginBase.cs (razor).
///
/// This is to enable composition over inheritance.
/// </summary>
/// <remarks>
/// It is re-architected so that it doesn't do unnecessary work,
/// like look for paths etc. which are not being used.
/// </remarks>
/// <param name="authStateProvider">
/// Lazy injection of the IdentityAuthenticationStateProvider.
/// For reasons not understood by cre8magic, this service cannot be directly injected.
/// Seems to be a timing issue.
/// </param>
internal class OqtaneLoginHelperWip(
    NavigationManager navigationManager,
    ISettingService settingService,
    IUserService userService,
    ILogService loggingService,
    MagicLazy<IdentityAuthenticationStateProvider> authStateProvider,
    SiteState siteState,
    IJSRuntime jsRuntime,
    IServiceProvider ServiceProvider
    )
{
    private bool AllowExternalLogin(PageState pageState)
    {
        if (_allowExternalLogin) return _allowExternalLogin;
        _allowExternalLogin = settingService.GetSetting(pageState.Site.Settings, "ExternalLogin:ProviderType", "") != "";
        return _allowExternalLogin;
    }

    private bool _allowExternalLogin;

    private bool AllowSiteLogin(PageState pageState)
    {
        if (_allowSiteLogin) return _allowSiteLogin;
        _allowSiteLogin = bool.Parse(settingService.GetSetting(pageState.Site.Settings, "LoginOptions:AllowSiteLogin", "true"));
        return _allowSiteLogin;
    }

    private bool _allowSiteLogin;

    public string LoginUrl(PageState pageState)
    {
        var loginUrl = AllowExternalLogin(pageState) && !AllowSiteLogin(pageState)
            ?
            // external login
            Utilities.TenantUrl(pageState.Alias, "/pages/external")
            :
            // local login
            NavigateUrl(pageState, "login");


        loginUrl += "?returnurl="
                    + (pageState.QueryString.TryGetValue("returnurl", out var returnUrl)
                        // use existing value
                        ? returnUrl
                        // remember current url
                        : WebUtility.UrlEncode(pageState.Route.PathAndQuery));

        return loginUrl;
    }

    public string LogoutUrl(PageState pageState) =>
        Utilities.TenantUrl(pageState.Alias, "/pages/logout/");

    public string ReturnUrl(PageState pageState)
    {
        var anonUsersCanAccessCurrentPage = UserSecurity.IsAuthorized(null, PermissionNames.View, pageState.Page.PermissionList);
        var pageIsVisibleAccordingToExpiry = Utilities.IsEffectiveAndNotExpired(pageState.Page.EffectiveDate, pageState.Page.ExpiryDate);
        return anonUsersCanAccessCurrentPage && pageIsVisibleAccordingToExpiry
            ? pageState.Route.PathAndQuery
            : pageState.Alias.Path;
    }

    public async Task ToggleLogin(PageState pageState)
    {
        if (pageState.User is { IsAuthenticated: true })
            await LogoutUser(pageState);
        else
            LoginUser(pageState);
    }

    public void LoginUser(PageState pageState)
    {
        if (AllowExternalLogin(pageState) && !AllowSiteLogin(pageState))
        {
            // external login
            navigationManager.NavigateTo(LoginUrl(pageState), true);
        }
        else
        {
            // local login
            navigationManager.NavigateTo(LoginUrl(pageState));
        }
    }

    public async Task LogoutUser(PageState pageState)
    {
        await loggingService.Log(pageState.Alias, pageState.Page.PageId, null, pageState.User?.UserId, GetType().AssemblyQualifiedName, "Logout", LogFunction.Security, LogLevel.Information, null, "User Logout For Username {Username}", pageState.User?.Username);

        if (pageState.Runtime == Runtime.Hybrid)
        {
            // hybrid apps utilize an interactive logout
            await userService.LogoutUserAsync(pageState.User);
            authStateProvider.Value.NotifyAuthenticationChanged();
            navigationManager.NavigateTo(ReturnUrl(pageState), true);
        }
        else // this condition is only valid for legacy Login button inheriting from LoginBase
        {
            // post to the Logout page to complete the logout process
            var fields = new { __RequestVerificationToken = siteState.AntiForgeryToken, returnurl = ReturnUrl(pageState) };
            var interop = new Interop(jsRuntime);
            await interop.SubmitForm(LogoutUrl(pageState), fields);
        }
    }

    //public string NavigateUrl(PageState pageState)
    //{
    //    return NavigateUrl(pageState, pageState.Page.Path);
    //}

    //public string NavigateUrl(PageState pageState, string path)
    //{
    //    return NavigateUrl(pageState, path, "");
    //}

    //public string NavigateUrl(PageState pageState, bool refresh)
    //{
    //    return NavigateUrl(pageState, pageState.Page.Path, refresh);
    //}

    public string NavigateUrl(PageState pageState, string? path = default, string querystring = "")
    {
        return Utilities.NavigateUrl(pageState.Alias.Path, path ?? pageState.Page.Path, querystring);
    }

    //public string NavigateUrl(PageState pageState, string path, bool refresh)
    //{
    //    return NavigateUrl(pageState, path, refresh ? "refresh" : "");
    //}

}