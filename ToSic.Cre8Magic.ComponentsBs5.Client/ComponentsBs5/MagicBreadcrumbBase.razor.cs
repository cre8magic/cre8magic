using Microsoft.AspNetCore.Components;
using Oqtane.UI;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.ComponentsBs5.Internal;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.ComponentsBs5;

/// <summary>
/// Recommended base class for all breadcrumb components.
/// </summary>
public abstract class MagicBreadcrumbBase: ComponentBase
{
    [Inject] public required IMagicHat MagicHat { get; set; }

    /// <inheritdoc cref="ComponentDocs.PageState"/>
    [CascadingParameter] public required PageState PageState { get; set; }

    /// <summary>
    /// Settings for retrieving the breadcrumbs; optional.
    /// If not set, the current page will be used as the active page.
    /// </summary>
    [Parameter] public MagicBreadcrumbSettings? Settings { get; set; }

    [Parameter] public virtual IMagicPageDesigner? Designer { get; set; }

    // TODO: note also that we're using BreadcrumbKit.Pages.Classes(...) somewhere, so we should add the designer to the kit

    /// <summary>
    /// The Breadcrumb for the current page.
    /// Will be updated when the page changes.
    /// </summary>
    protected IMagicBreadcrumbKit BreadcrumbKit => _breadcrumbKit.Get(PageState, () => MagicHat.BreadcrumbKit(Settings.With(PageState).With(Designer)));
    private readonly GetKeepByPageId<IMagicBreadcrumbKit> _breadcrumbKit = new();

}