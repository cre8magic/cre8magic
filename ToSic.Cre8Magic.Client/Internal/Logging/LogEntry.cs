using ToSic.Cre8magic.Utils.Internal;

namespace ToSic.Cre8magic.Internal.Logging;

internal class LogEntry(ILog? log, string? message, int depth, CodeRef codeRef)
{
    public string Source { get; } = log?.Prefix ?? "";

    public string? Message { get; } = message;

    public ILog? Log { get; } = log;
    public CodeRef CodeRef { get; } = codeRef;

    public string? Result { get; private set; }

    public int Depth = depth;
    
    public object? Data { get; set; }

    public void AppendResult(string? message) => Result = message;


    public override string ToString()
    {
        var indent = new string('>', Math.Max(0, Depth - 1));
        if (indent.HasValue()) indent += " ";
        var result = $"{Source}{(Source.HasValue() ? ": " : "")}" +
               indent +
               $"{Message}" +
               $"{(Result.HasValue() ? $"='{Result}'" : "")}";

        return result;
    }
}