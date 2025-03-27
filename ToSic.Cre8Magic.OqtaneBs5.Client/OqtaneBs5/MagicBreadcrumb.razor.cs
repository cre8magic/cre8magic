using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Oqtane.Documentation;
using Oqtane.UI;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.OqtaneBs5;

// INFO for Tonci
// This must use a code-behind, otherwise it won't show up in the docs.

/// <summary>
/// A breadcrumb component for Oqtane using Bootstrap 5, which can be used to show the current page's position in the site hierarchy.
/// </summary>
/// <remarks>
/// It's quite flexible and provides a lot of configuration options.
///
/// This is the Bootstrap5 implementation, so it uses the standard Bootstrap 5 hierarchy and classes,
/// but you can customize the output by providing your own tailor.
///
/// The output will be:
///
/// <code language="html">
/// &lt;nav aria-label="breadcrumb"&gt;
///     &lt;ol class="..."&gt;
///         &lt;li class="..."&gt;&lt;a href="..." class="..."&gt;...&lt;/a&gt;&lt;/li&gt;
///     &lt;/ol&gt;
/// &lt;/nav&gt;
/// </code>
/// </remarks>
public partial class MagicBreadcrumb : ComponentBase
{
    [Parameter]
    public string? Name { get; set; }

    [PrivateApi]
    [CascadingParameter]
    public required PageState PageState { get; set; }

    [Parameter]
    public MagicBreadcrumbSettings? Settings { get; set; }

    [PrivateApi]
    [Inject]
    public IMagicAct MagicAct { get; set; } = null!;

    // TODO: note also that we're using BreadcrumbKit.Pages.Classes(...) somewhere, so we should add the designer to the kit

    [PrivateApi]
    protected IMagicBreadcrumbKit GetBreadcrumbKit() =>
        MagicAct.BreadcrumbKit(
            Settings
                .With(PageState, Name)
                .Refill((IMagicPageTailor)new MagicBreadcrumbTailorBs5())
        );
}