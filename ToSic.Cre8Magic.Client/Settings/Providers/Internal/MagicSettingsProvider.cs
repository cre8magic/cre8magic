using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Providers.Internal;

internal class MagicSettingsProvider: IMagicSettingsProvider, IMagicSettingsSource
{
    int IMagicSettingsSource.Priority => 200;

    /// <summary>
    /// Implement interface to get this stuff
    /// </summary>
    /// <param name="packageSettings"></param>
    /// <returns></returns>
    public SettingsSourceInfo? Get(MagicPackageSettings packageSettings)
    {
        if (Containers?.HasValues != true && Breadcrumbs?.HasValues != true && Analytics?.HasValues != true)
            return null;

        var catalog = new MagicSettingsCatalog
        {
            Analytics = _analytics?.Values != null ? new(_analytics.Values) : new(),
            Breadcrumbs = _breadcrumbs?.Values != null ? new(_breadcrumbs.Values) : new(),
            Containers = _containers?.Values != null ? new(_containers.Values) : new(),
            MenuDesigns = _menuDesigns?.Values != null
                ? new(_menuDesigns.Values.ToDictionary(
                    dic => dic.Key,
                    dic => new NamedSettings<MagicMenuDesignSettings>(dic.Value)
                ))
                : new()
        };

        return new(catalog, null);
    }

    public /* actually internal */ void Reset()
    {
        _analytics = null;
        _containers = null;
        _breadcrumbs = null;
        _menuDesigns = null;
    }

    public IMagicProviderSection<MagicAnalyticsSettings> Analytics => _analytics ??= new(this);
    private MagicProviderSection<MagicAnalyticsSettings>? _analytics;

    public IMagicProviderSection<MagicBreadcrumbSettings> Breadcrumbs => _breadcrumbs ??= new(this);
    private MagicProviderSection<MagicBreadcrumbSettings>? _breadcrumbs;

    public IMagicProviderSection<MagicContainerSettings> Containers => _containers ??= new(this);
    private MagicProviderSection<MagicContainerSettings>? _containers;

    public IMagicProviderSection<Dictionary<string, MagicMenuDesignSettings>> MenuDesigns => _menuDesigns ??= new(this);
    private MagicProviderSection<Dictionary<string, MagicMenuDesignSettings>>? _menuDesigns;

}