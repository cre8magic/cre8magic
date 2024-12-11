using Oqtane.UI;

namespace ToSic.Cre8magic.Analytics.Internal;

/// <summary>
/// Service to help with Google Analytics Tracking.
/// </summary>
public interface IMagicAnalyticsService
{
    IMagicAnalyticsKit AnalyticsKit(PageState pageState, MagicAnalyticsSpell? settings = default);
}