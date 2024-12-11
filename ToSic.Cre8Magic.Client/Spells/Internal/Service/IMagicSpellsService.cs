using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Spells.Debug;
using ToSic.Cre8magic.Spells.Internal.Debug;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Spells.Internal;

public interface IMagicSpellsService
{
    /// <summary>
    /// Set up the settings service with the package settings, layout name and body classes.
    /// This will result in other controls and services being able to use these settings.
    /// Otherwise, the settings are just defaulted to some standard values.
    /// </summary>
    /// <param name="themePackage"></param>
    /// <returns></returns>
    IMagicSpellsService Setup(MagicThemePackage themePackage);

    /// <summary>
    /// Get lightweight theme context - basically the final name, settings and journal.
    /// </summary>
    /// <param name="pageStateForCachingOnly"></param>
    /// <returns></returns>
    internal CmThemeContext GetThemeContext(PageState? pageStateForCachingOnly);

    internal CmThemeContextFull GetThemeContextFull(PageState pageState);

    // TODO: MAKE INTERNAL again - temporarily public soi
    public MagicDebugSettings Debug { get; }

    internal List<DataWithJournal<MagicSpellsBook>> Library { get; }


    internal SettingsReader<MagicAnalyticsSpell> Analytics { get; }
    internal SettingsReader<MagicThemeBlueprint> ThemeBlueprints { get; }

    internal SettingsReader<MagicLanguageSpell> Languages { get; }
    internal SettingsReader<MagicLanguageBlueprint> LanguageBlueprints { get; }



    internal SettingsReader<MagicMenuSpell> Menus { get; }
    internal SettingsReader<MagicMenuBlueprint> MenuBlueprints { get; }




    internal SettingsReader<MagicBreadcrumbSpell> Breadcrumbs { get; }
    internal SettingsReader<MagicBreadcrumbBlueprint> BreadcrumbBlueprints { get; }


    internal SettingsReader<MagicPageContextSpell> PageContexts { get; }
    internal SettingsReader<MagicContainerSpell> Containers { get; }
    internal SettingsReader<MagicContainerBlueprint> ContainerBlueprints { get; }

    /// <summary>
    /// WIP: PageState for this service
    /// </summary>
    PageState? PageState { get; }

    MagicThemePackage ThemePackage { get; }

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

    IMagicSpellsService UsePageState(PageState pageState);
}