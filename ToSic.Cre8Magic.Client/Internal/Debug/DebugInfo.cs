using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Spells;

namespace ToSic.Cre8magic.Internal.Debug;

public record DebugInfo
{
    public required string Title { get; init; }

    public Dictionary<string, object?> More { get; init; } = new();

    public Dictionary<string, string?>? Values { get; init; }

    public required MagicInheritsBase Settings { get; init; }

    internal static string ShowNotSet(string? value) =>
        string.IsNullOrWhiteSpace(value) ? "---" : value;
}