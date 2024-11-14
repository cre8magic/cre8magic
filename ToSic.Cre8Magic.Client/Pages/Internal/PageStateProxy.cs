using Oqtane.Models;
using Oqtane.Shared;
using Oqtane.UI;

namespace ToSic.Cre8magic.Pages.Internal;

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
    // Do extra stuff here if needed

    public bool Debug { get; }

    public PageState PageState { get; }
}

public static class PageStateProxyExtensions
{
    public static PageStateProxy ToProxy(this PageState pageState, bool debug = false)
    {
        return new PageStateProxy(pageState, debug);
    }

    public static bool IsDebug(this PageState pageState) => pageState is PageStateProxy { Debug: true };

    public static void DoNothing(this PageState pageState) { }
}