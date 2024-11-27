using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Internal.Providers;

internal class MagicSettingsProvider: IMagicSettingsProvider, IMagicSettingsSource
{
    int IMagicSettingsSource.Priority => 200;

    /// <summary>
    /// Implement interface to get this stuff
    /// </summary>
    /// <param name="themePackage"></param>
    /// <returns></returns>
    public List<DataWithJournal<MagicSettingsCatalog>> Catalogs(MagicThemePackage themePackage)
    {
        var partsNoData = AllSources.All(source => source?.HasValues != true);
        if (partsNoData)
            return _catalog == null
                ? []
                : [new(_catalog, new())];

        var catalog = _catalog ?? new MagicSettingsCatalog();

        if (!partsNoData)
            catalog = catalog with
            {
                Analytics = _analytics?.Values != null ? new(_analytics.Values) : catalog.Analytics,
                Breadcrumbs = _breadcrumbs?.Values != null ? new(_breadcrumbs.Values) : catalog.Breadcrumbs,
                Containers = _containers?.Values != null ? new(_containers.Values) : catalog.Containers,
                MenuDesigns = _menuDesigns?.Values != null
                    ? new(_menuDesigns.Values.ToDictionary(
                        dic => dic.Key,
                        dic => new MagicMenuDesignSettings(dic.Value)
                    ))
                    : catalog.MenuDesigns,
                Themes = _themes?.Values != null ? new(_themes.Values) : catalog.Themes,
            };

        return [new(catalog, new())];
    }

    public void Reset()
    {
        foreach (var source in AllSources) 
            source?.Reset();
    }

    /// <summary>
    /// Remember to add any new sources to this list!
    /// </summary>
    private List<ISourceInternal?> AllSources =>
    [
        _analytics,
        _containers,
        _breadcrumbs,
        _menuDesigns,
        _themes
    ];

    public IMagicSettingsProvider Provide(MagicSettingsCatalog catalog)
    {
        _catalog = catalog;
        return this;
    }

    public MagicSettingsCatalog? Catalog => Catalogs(null!).FirstOrDefault()?.Data;

    private MagicSettingsCatalog? _catalog;

    public IMagicSettingsProviderSection<MagicAnalyticsSettings> Analytics => _analytics ??= new(this);
    private MagicSettingsProviderSection<MagicAnalyticsSettings>? _analytics;

    public IMagicSettingsProviderSection<MagicBreadcrumbSettings> Breadcrumbs => _breadcrumbs ??= new(this);
    private MagicSettingsProviderSection<MagicBreadcrumbSettings>? _breadcrumbs;

    public IMagicSettingsProviderSection<MagicContainerSettings> Containers => _containers ??= new(this);
    private MagicSettingsProviderSection<MagicContainerSettings>? _containers;

    public IMagicSettingsProviderSection<MagicMenuDesignSettings> MenuDesigns => _menuDesigns ??= new(this);
    private MagicSettingsProviderSection<MagicMenuDesignSettings>? _menuDesigns;

    public IMagicSettingsProviderSection<MagicThemeSettings> Themes => _themes ??= new(this);
    private MagicSettingsProviderSection<MagicThemeSettings>? _themes;
}