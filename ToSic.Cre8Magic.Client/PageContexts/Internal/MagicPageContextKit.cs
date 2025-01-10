using Oqtane.UI;

namespace ToSic.Cre8magic.PageContexts.Internal;

/// <summary>
/// implementation...
/// </summary>
internal record MagicPageContextKit : IMagicPageContextKit
{
    public bool UseBodyTag => Settings.UseBodyTagSafe;

    public string? TagId => Settings.TagId;

    public required string? Classes { get; init; }

    public required MagicPageContextSettings Settings { get; init; }

    internal required PageState PageState { get; init; }
    internal required MagicPageContextService Service { get; init; }

    public async Task UpdateBodyTag() =>
        await Service.UpdateBodyTag(PageState, Settings);
}