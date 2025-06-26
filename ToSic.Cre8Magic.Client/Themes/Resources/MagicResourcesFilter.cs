using Oqtane.Shared;

namespace ToSic.Cre8magic.Themes;

public record MagicResourcesFilter
{
    public ResourceType? ResourceType { get; init; } = null;

    public MagicControls ForControls { get; init; } = MagicControls.All;
}