using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Spells;

namespace ToSic.Cre8magic.Links;

public record MagicLinkSettings: MagicSpellBase
{
    public string? Path { get; init; }

    public string? QueryString { get; init; }
}