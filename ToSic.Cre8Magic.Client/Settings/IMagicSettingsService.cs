using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Settings;

public interface IMagicSettingsService: IHasSystemMessages
{
    /// <summary>
    /// Set up the settings service with the package settings, layout name and body classes.
    /// This will result in other controls and services being able to use these settings.
    /// Otherwise, the settings are just defaulted to some standard values.
    /// </summary>
    /// <param name="packageSettings"></param>
    /// <param name="layoutName"></param>
    /// <returns></returns>
    IMagicSettingsService Setup(MagicPackageSettings packageSettings, string? layoutName);

    /// <summary>
    /// Get lightweight theme context - basically the final name, settings and journal.
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    internal MagicThemeContext GetThemeContext(PageState pageState);

    internal MagicThemeContextFull GetThemeContextFull(PageState pageState);

    // #WipRemovingPreMergedCatalog
    //internal MagicSettingsCatalog Catalog { get; }

    internal MagicDebugSettings Debug { get; }

    internal NamedSettingsReader<MagicAnalyticsSettings> Analytics { get; }
    internal NamedSettingsReader<MagicThemeDesignSettings> ThemeDesign { get; }

    internal NamedSettingsReader<MagicLanguageSettings> Languages { get; }

    internal NamedSettingsReader<Dictionary<string, MagicMenuDesignSettings>> MenuDesigns { get; }

    internal NamedSettingsReader<MagicMenuSettingsData> MenuSettings { get; }
    internal List<MagicSettingsCatalog> AllCatalogs { get; }

    internal TokenEngine PageTokenEngine(PageState pageState);

    ///// <summary>
    ///// Figure out which settings-name can be used.
    ///// It will first try the preferred option, then the fallback, and if that doesn't exist, it will use the default name.
    ///// </summary>
    ///// <param name="preferred"></param>
    ///// <param name="fallback"></param>
    ///// <returns></returns>
    //internal (string BestName, List<string> Journal) GetBestSettingsName(string? preferred, string fallback);
    MagicDebugState DebugState(PageState pageState);


    MagicAnalyticsSettings AnalyticsSettings(string settingsName);

    internal TDebug BypassCacheInternal<TDebug>(Func<IMagicSettingsService, TDebug> func);
}