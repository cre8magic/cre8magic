using Oqtane.UI;

namespace ToSic.Cre8magic.PageContexts.Internal;

/// <summary>
/// implementation...
/// </summary>
internal record MagicPageContextKit : IMagicPageContextKit
{
    public bool UseBodyTag => Spell.UseBodyTagSafe;

    public string? TagId => Spell.TagId;

    public required string? Classes { get; init; }

    public required MagicPageContextSpell Spell { get; init; }

    internal required PageState PageState { get; init; }
    internal required MagicPageContextService Service { get; init; }

    public async Task UpdateBodyTag() =>
        await Service.UpdateBodyTag(PageState, Spell);
}