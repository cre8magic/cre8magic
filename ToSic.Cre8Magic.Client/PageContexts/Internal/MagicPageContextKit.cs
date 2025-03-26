using System.Diagnostics.CodeAnalysis;
using Oqtane.UI;

namespace ToSic.Cre8magic.PageContexts.Internal;

/// <summary>
/// implementation...
/// </summary>
internal record MagicPageContextKit : IMagicPageContextKit
{
    public bool UseBodyTag => Stable.UseBodyTag;

    public string? TagId => Stable.TagId;

    public required string? Classes { get; init; }

    public required MagicPageContextSettings Settings { get; init; }

    [field: AllowNull, MaybeNull]
    private MagicPageContextSettings.Stabilized Stable => field ??= Settings.GetStable();

    internal required PageState PageState { get; init; }
    internal required MagicPageContextService Service { get; init; }

    public async Task UpdateBodyTag() =>
        await Service.UpdateBodyTag(PageState, Settings);
}