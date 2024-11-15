using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Utils.Logging;

public class LogRoot()
{
    [JsonIgnore]
    internal readonly List<LogEntry> LogEntries = [];

    public IEnumerable<object?> Entries => LogEntries.SelectMany(e =>
    {
        if (e?.Data == null) return new [] { e?.ToString() as object};
        return new[] { e?.ToString(), new { e.Data } as object };
    });

    [JsonIgnore]
    public int Depth { get; set; } = 0;

    internal Log GetLog(string? prefix) => new(this, Depth + 1, prefix);
}