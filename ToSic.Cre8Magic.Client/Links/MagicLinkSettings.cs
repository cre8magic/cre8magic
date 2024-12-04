using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Links;

public record MagicLinkSettings: MagicSettingsBase
{
    public string? Path { get; init; }

    public string? QueryString { get; init; }
}