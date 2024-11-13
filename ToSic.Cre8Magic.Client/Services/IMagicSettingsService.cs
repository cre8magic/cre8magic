using Microsoft.Extensions.Logging;
using Oqtane.UI;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Languages.Settings;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Services.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Client.Services;

public interface IMagicSettingsService: IHasSettingsExceptions
{
    /// <summary>
    /// Set up the settings service with the package settings, layout name and body classes.
    /// This will result in other controls and services being able to use these settings.
    /// Otherwise, the settings are just defaulted to some standard values.
    /// </summary>
    /// <param name="packageSettings"></param>
    /// <param name="layoutName"></param>
    /// <param name="bodyClasses"></param>
    /// <returns></returns>
    IMagicSettingsService Setup(MagicPackageSettings packageSettings, string? layoutName, string? bodyClasses);

    MagicAllSettings GetSettings(PageState pageState);

    internal MagicSettingsCatalog Catalog { get; }

    internal ILogger<IMagicSettingsService> Logger { get; }

    internal MagicDebugSettings Debug { get; }

    internal NamedSettingsReader<MagicAnalyticsSettings> Analytics { get; }
    internal NamedSettingsReader<MagicThemeDesignSettings> ThemeDesign { get; }

    internal NamedSettingsReader<MagicLanguagesSettings> Languages { get; }

    internal NamedSettingsReader<NamedSettings<MagicMenuDesignSettings>> MenuDesigns { get; }

    internal NamedSettingsReader<MagicMenuSettings> MenuSettings { get; }

    internal (string ConfigName, List<string> Source) FindConfigName(string? configName, string inheritedName);
}