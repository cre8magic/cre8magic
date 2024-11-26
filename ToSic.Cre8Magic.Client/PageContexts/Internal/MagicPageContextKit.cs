using Oqtane.UI;

namespace ToSic.Cre8magic.PageContexts.Internal;

/// <summary>
/// implementation...
/// </summary>
internal record MagicPageContextKit : IMagicPageContextKit
{
    public required bool UseBodyTag { get; init; }

    public required string? TagId { get; init; }

    public required string? Classes { get; init; }

    public required MagicPageContextSettings? Settings { get; init; }

    internal required PageState PageState { get; init; }
    internal required MagicPageContextService Service { get; init; }

    public async Task UpdateBodyTag() =>
        await Service.UpdateBodyTag(PageState, Settings);
}