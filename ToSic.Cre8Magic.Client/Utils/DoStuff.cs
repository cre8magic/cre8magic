﻿namespace ToSic.Cre8magic.Utils;

internal class DoStuff
{
    public static void IgnoreError(Action action)
    {
        try
        {
            action?.Invoke();
        }
        catch
        {
            /* ignore */
        }
    }

    /// <summary>
    /// Run an async Command but don't wait for it
    /// https://stackoverflow.com/questions/17805887/using-async-without-await
    /// </summary>
    /// <returns></returns>
    public static async Task DoNotWait(Action action) =>
        await Task.Run(() => action?.Invoke());

    public static async Task DoNotWaitAndIgnoreErrors(Action action) =>
        await Task.Run(() => IgnoreError(() => action?.Invoke()));
}