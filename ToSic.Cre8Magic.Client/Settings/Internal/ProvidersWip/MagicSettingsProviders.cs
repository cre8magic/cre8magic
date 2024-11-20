using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Internal;

internal class MagicSettingsProviders: IMagicSettingsProviders, IMagicSettingsSource
{
    int IMagicSettingsSource.Priority => 200;

    public SettingsSourceInfo? Get(MagicPackageSettings packageSettings)
    {
        if (Containers?.HasValues != true && Breadcrumbs?.HasValues != true && Analytics?.HasValues != true)
            return null;

        var catalog = new MagicSettingsCatalog
        {
            Analytics = Analytics?.Values != null ? new(Analytics.Values) : new(),
            Breadcrumbs = Breadcrumbs?.Values != null ? new(Breadcrumbs.Values) : new(),
            Containers = Containers?.Values != null ? new(Containers.Values) : new()
        };

        return new(catalog, null);
    }

    public MagicSettingsProvider<MagicAnalyticsSettings> Analytics => _analyticsSettings ??= new(this);
    private MagicSettingsProvider<MagicAnalyticsSettings>? _analyticsSettings;

    public MagicSettingsProvider<MagicContainerSettings> Containers => _containerSettings ??= new(this);
    private MagicSettingsProvider<MagicContainerSettings>? _containerSettings;

    public MagicSettingsProvider<MagicBreadcrumbSettings> Breadcrumbs => _breadcrumbSettings ??= new(this);
    private MagicSettingsProvider<MagicBreadcrumbSettings>? _breadcrumbSettings;


}