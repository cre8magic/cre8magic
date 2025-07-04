﻿using System.Runtime.CompilerServices;

namespace ToSic.Cre8magic.Internal.Logging;

internal static class LogCallBaseExtensions
{
    public static void A(this LogCallBase logCall,
        string message,
        [CallerFilePath] string cPath = null!,
        [CallerMemberName] string cName = null!,
        [CallerLineNumber] int cLine = 0
        // ReSharper disable ExplicitCallerInfoArgument
    ) => logCall?.LogOrNull?.A(message, cPath, cName, cLine);
    // ReSharper restore ExplicitCallerInfoArgument

    internal static void DoneInternal(this LogCallBase? logCall, string? message)
    {
        if (logCall?.LogOrNull == null)
            return;
        var log = logCall.LogOrNull;

        //if (!logCall.IsOpen)
        //    log.AddInternal("Log Warning: Wrapper already closed from previous call", null);
        //logCall.IsOpen = false;

        //log.WrapDepth--;
        logCall.Entry?.AppendResult(message);
        // var final = log.AddInternalReuse(null, null);
        //final.WrapClose = true;
        log.Depth--;
        // final.AppendResult(message);
        //if (logCall.Stopwatch == null) return;
        //logCall.Stopwatch.Stop();
        //logCall.Entry.Elapsed = logCall.Stopwatch.Elapsed;
    }
}