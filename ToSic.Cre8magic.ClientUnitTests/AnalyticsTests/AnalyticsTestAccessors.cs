using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.AnalyticsTests;

internal static class AnalyticsTestAccessors
{
    public static T CloneUnderTac<T>(this ICanClone<T> fallback, T? priority, bool forceCopy = false)
        => fallback.CloneUnder(priority, forceCopy);
}