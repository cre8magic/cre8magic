using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Internal.Logging;

public class LogRoot
{
    [JsonIgnore]
    internal readonly List<LogEntry> LogEntries = [];

    public IEnumerable<object?> Entries => LogEntries.SelectMany(entry =>
        entry?.Data == null
            ? new[] { entry?.ToString() as object }
            : [entry.ToString(), new { entry.Data }]
    );

    [JsonIgnore]
    public int Depth { get; set; } = 0;

    internal Log GetLog(string? prefix) => new(this, Depth + 1, prefix);

    internal void Add(string name, IEnumerable<string> journal)
    {
        if (name == null || journal == null) return;
        if (!journal.Any()) return;
        var messageLog = GetLog(name);
        foreach (var m in journal) messageLog.A(m);

    }
}