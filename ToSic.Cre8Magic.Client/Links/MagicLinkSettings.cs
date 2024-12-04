using Oqtane.UI;

namespace ToSic.Cre8magic.Links;

public record MagicLinkSettings
{
    public string? Path { get; init; }

    public string? QueryString { get; init; }

    public PageState? PageState { get; init; }
}