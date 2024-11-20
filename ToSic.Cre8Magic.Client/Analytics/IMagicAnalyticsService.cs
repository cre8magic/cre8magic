using Oqtane.UI;

namespace ToSic.Cre8magic.Analytics;

/// <summary>
/// Service to help with Google Analytics Tracking.
/// </summary>
public interface IMagicAnalyticsService
{
    IMagicAnalyticsKit AnalyticsKit(PageState pageState, MagicAnalyticsSettings? settings = default);
}