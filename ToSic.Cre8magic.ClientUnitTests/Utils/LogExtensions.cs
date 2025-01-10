using ToSic.Cre8magic.Internal.Logging;
using Xunit.Abstractions;

namespace ToSic.Cre8magic.ClientUnitTests.Utils;

internal static class LogExtensions
{
    public static void Dump(this LogRoot logRoot, ITestOutputHelper output)
    {
        output.WriteLine("");
        output.WriteLine("LOG");
        output.WriteLine(string.Join('\n', logRoot.LogEntries));
    }
}