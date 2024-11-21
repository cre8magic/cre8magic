using Oqtane.UI;

namespace ToSic.Cre8magic.Settings.Internal;

internal static class MagicSettingsExtensions
{
    public static DataWithJournal<(string MainName, string BestSettingsName, string BestDesignName)> GetMostRelevantNames(
        this IMagicSettingsService settings,
        PageState pageState,
        string? partName,
        string? prefix
    )
    {
        var themeCtx = settings.GetThemeContext(pageState);
        var ((bestSettingsName, bestDesignName), journal) = themeCtx.NameResolver.GetBestNames(partName, prefix);
        return new((themeCtx.SettingsName, bestSettingsName, bestDesignName), journal);
    }
}