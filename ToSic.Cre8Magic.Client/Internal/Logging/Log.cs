﻿namespace ToSic.Cre8magic.Internal.Logging;

/// <summary>
/// Very trivial Logger - which is actually just a settings how to log.
/// The logging happens in extension methods, and the entries are actually placed in a LogRoot.
/// </summary>
internal class Log: ILog
{
    public LogRoot LogRoot { get; }
    public string? Prefix { get; }

    internal Log(LogRoot logRoot, int depth, string? prefix)
    {
        LogRoot = logRoot;
        Prefix = prefix;
        Depth = depth;
    }

    public int Depth { get; set; }
}