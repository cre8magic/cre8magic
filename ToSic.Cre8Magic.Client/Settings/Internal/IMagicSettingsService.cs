using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Settings;

public interface IMagicSettingsService
{
    /// <summary>
    /// Set up the settings service with the package settings, layout name and body classes.
    /// This will result in other controls and services being able to use these settings.
    /// Otherwise, the settings are just defaulted to some standard values.
    /// </summary>
    /// <param name="themePackage"></param>
    /// <param name="layoutName"></param>
    /// <returns></returns>
    IMagicSettingsService Setup(MagicThemePackage themePackage, string? layoutName);

    /// <summary>
    /// Get lightweight theme context - basically the final name, settings and journal.
    /// </summary>
    /// <param name="pageStateForCachingOnly"></param>
    /// <returns></returns>
    internal CmThemeContext GetThemeContext(PageState? pageStateForCachingOnly);

    internal CmThemeContextFull GetThemeContextFull(PageState pageState);

    // TODO: MAKE INTERNAL again - temporarily public soi
    public MagicDebugSettings Debug { get; }

    internal List<DataWithJournal<MagicSettingsCatalog>> Catalogs { get; }


    internal SettingsReader<MagicAnalyticsSettings> Analytics { get; }
    internal SettingsReader<MagicThemeDesignSettings> ThemeDesigns { get; }

    internal SettingsReader<MagicLanguageSettings> Languages { get; }
    internal SettingsReader<MagicLanguageDesignSettings> LanguageDesigns { get; }



    internal SettingsReader<MagicMenuSettings> Menus { get; }
    internal SettingsReader<MagicMenuDesignSettings> MenuDesigns { get; }




    internal SettingsReader<MagicBreadcrumbSettings> Breadcrumbs { get; }
    internal SettingsReader<MagicBreadcrumbBlueprint> BreadcrumbBlueprints { get; }


    internal SettingsReader<MagicPageContextSettings> PageContexts { get; }
    internal SettingsReader<MagicContainerSettings> Containers { get; }
    internal SettingsReader<MagicContainerBlueprint> ContainerBlueprints { get; }

    /// <summary>
    /// WIP: PageState for this service
    /// </summary>
    PageState? PageState { get; }

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

    IMagicSettingsService UsePageState(PageState pageState);
}