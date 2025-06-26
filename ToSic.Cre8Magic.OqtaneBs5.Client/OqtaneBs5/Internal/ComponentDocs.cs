﻿using Microsoft.AspNetCore.Components;
using Oqtane.UI;

namespace ToSic.Cre8magic.OqtaneBs5.Internal;

/// <summary>
/// Just a helper class to store documentations for other components to inherit from.
/// </summary>
internal abstract class ComponentDocs
{
    /// <summary>
    /// The PageState containing the current page, list of all pages, user, etc.
    /// It will be injected by Blazor when the component is initialized using `[CascadingParameter]`.
    /// In most cases, this PageState is all you need.
    /// </summary>
    [CascadingParameter]
    public required PageState PageState { get; set; }

}