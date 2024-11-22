using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Sources;
using ToSic.Cre8magic.Themes;

namespace ToSic.Cre8magic.Settings.Providers.Internal;

internal class MagicSettingsProvider: IMagicSettingsProvider, IMagicSettingsSource
{
    int IMagicSettingsSource.Priority => 200;

    /// <summary>
    /// Implement interface to get this stuff
    /// </summary>
    /// <param name="packageSettings"></param>
    /// <returns></returns>
    public List<DataWithJournal<MagicSettingsCatalog>> Catalog(MagicPackageSettings packageSettings)
    {
        if (AllSources.All(source => source?.HasValues != true))
            return [];

        var catalog = new MagicSettingsCatalog
        {
            Analytics = _analytics?.Values != null ? new(_analytics.Values) : new(),
            Breadcrumbs = _breadcrumbs?.Values != null ? new(_breadcrumbs.Values) : new(),
            Containers = _containers?.Values != null ? new(_containers.Values) : new(),
            MenuDesigns = _menuDesigns?.Values != null
                ? new(_menuDesigns.Values.ToDictionary(
                    dic => dic.Key,
                    dic => new MagicMenuDesignSettings(dic.Value)
                ))
                : new(),
            Themes = _themes?.Values != null ? new(_themes.Values) : new()
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

    public IMagicProviderSection<MagicAnalyticsSettings> Analytics => _analytics ??= new(this);
    private MagicProviderSection<MagicAnalyticsSettings>? _analytics;

    public IMagicProviderSection<MagicBreadcrumbSettings> Breadcrumbs => _breadcrumbs ??= new(this);
    private MagicProviderSection<MagicBreadcrumbSettings>? _breadcrumbs;

    public IMagicProviderSection<MagicContainerSettings> Containers => _containers ??= new(this);
    private MagicProviderSection<MagicContainerSettings>? _containers;

    public IMagicProviderSection<MagicMenuDesignSettings> MenuDesigns => _menuDesigns ??= new(this);
    private MagicProviderSection<MagicMenuDesignSettings>? _menuDesigns;

    public IMagicProviderSection<MagicThemeSettings> Themes => _themes ??= new(this);
    private MagicProviderSection<MagicThemeSettings>? _themes;
}