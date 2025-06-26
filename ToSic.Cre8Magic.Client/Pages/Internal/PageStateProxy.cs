using Oqtane.UI;

namespace ToSic.Cre8magic.Pages.Internal;

/// <summary>
/// Wrapper for PageState, so that we can "inject" a debug state to a specific menu.
/// This is just for internal development.
///
/// Goal is that if we have a page with a lot of test-menus, we can provide
/// this instead of the real PageState, and then we can stop
/// in certain places - only for this menu.
///
/// To do that, see the extension methods below.
///
/// Usage ca.:
/// if (PageState.IsDebug()) PageState.DoNothing(); // set breakpoint here
/// </summary>
public class PageStateProxy : PageState
{
    public PageStateProxy(PageState pageState, bool debug = false)
    {
        PageState = pageState;
        Debug = debug;

        // Transfer all fields of the original to the properties

        Alias = pageState.Alias;
        Site = pageState.Site;
        Page = pageState.Page;
        User = pageState.User;
        Uri = pageState.Uri;
        Route = pageState.Route;
        QueryString = pageState.QueryString;
        UrlParameters = pageState.UrlParameters;
        ModuleId = pageState.ModuleId;
        Action = pageState.Action;
        EditMode = pageState.EditMode;
        LastSyncDate = pageState.LastSyncDate;
        RenderMode = pageState.RenderMode;
        Runtime = pageState.Runtime;
        VisitorId = pageState.VisitorId;
        RemoteIPAddress = pageState.RemoteIPAddress;
        ReturnUrl = pageState.ReturnUrl;
        IsInternalNavigation = pageState.IsInternalNavigation;
        RenderId = pageState.RenderId;
        Refresh = pageState.Refresh;
    }

    /// <summary>
    /// Debug state to check if we should debug this case.
    /// </summary>
    public bool Debug { get; }

    /// <summary>
    /// Underlying PageState, in case we think the proxy is broken.
    /// </summary>
    public PageState PageState { get; }
}

public static class PageStateProxyExtensions
{
    /// <summary>
    /// Convert to proxy
    /// </summary>
    public static PageStateProxy ToProxy(this PageState pageState, bool debug = false) => new(pageState, debug);

    /// <summary>
    /// Check if we should debug this.
    /// </summary>
    /// <param name="pageState"></param>
    public static bool IsDebug(this PageState pageState) => pageState is PageStateProxy { Debug: true };

    public static void DoNothing(this PageState pageState) { }
}