using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Services.Internal;

internal static class MagicSettingsExtensions
{
    public static (string MainName, string BestSettingsName, string BestDesignName, List<string> Messages) GetMostRelevantNames(
        this IMagicSettingsService settings,
        PageState pageState,
        string? partName,
        string? prefix
    )
    {
        var themeCtx = settings.GetThemeContext(pageState);
        var nameResolver = new ThemePartNameResolver(themeCtx);
        var (bestSettingsName, bestDesignName, messages) = nameResolver.GetBestNames(partName, prefix);
        return (themeCtx.SettingsName, bestSettingsName, bestDesignName, messages);
    }
}