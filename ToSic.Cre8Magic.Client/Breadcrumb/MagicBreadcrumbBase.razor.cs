using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Breadcrumb.Settings;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Breadcrumb;

/// <summary>
/// Recommended base class for all breadcrumb components.
/// </summary>
public abstract class MagicBreadcrumbBase: MagicControlBase
{
    [Inject]
    public IMagicPageService? PageSvc { get; set; }

    /// <summary>
    /// Settings for retrieving the breadcrumbs; optional.
    /// If not set, the current page will be used as the active page.
    /// </summary>
    [Parameter]
    public MagicBreadcrumbSettings? Settings { get; set; }


    /// <summary>
    /// WIP experimental pattern. Probably not the best/final implementation yet...
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    protected virtual MagicBreadcrumbSettings Customize(MagicBreadcrumbSettings settings)
        => settings;

    // The home page - never changes during runtime, so we can cache it
    protected IMagicPage HomePage => _homePage ??= PageFactory.Home;
    private IMagicPage? _homePage;

    /// <summary>
    /// The Breadcrumb for the current page.
    /// Will be updated when the page changes.
    /// </summary>
    protected IMagicPageList Breadcrumb => _breadcrumbs.Get(
        () => (PageSvc.Setup(PageState).GetBreadcrumb(Customize(Settings ?? new())), PageState.Page.PageId),
        //() => (PageFactory.Breadcrumb.Get(), PageState.Page.PageId),
        (_, i) => i == PageState.Page.PageId
    );
    private readonly GetKeep<IMagicPageList, int?> _breadcrumbs = new();

}