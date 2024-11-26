using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Provider to give settings.
///
/// It is scoped, so anything added to it - typically in the Theme,
/// will be available in all other components.
/// </summary>
public interface IMagicSettingsProvider
{
    IMagicSettingsProviderSection<MagicContainerSettings> Containers { get; }

    IMagicSettingsProviderSection<MagicBreadcrumbSettingsData> Breadcrumbs { get; }
    IMagicSettingsProviderSection<MagicAnalyticsSettings> Analytics { get; }
    IMagicSettingsProviderSection<MagicMenuDesignSettings> MenuDesigns { get; }
    IMagicSettingsProviderSection<MagicThemeSettings> Themes { get; }
    public void Reset();
    IMagicSettingsProvider Provide(MagicSettingsCatalog catalog);

    internal MagicSettingsCatalog? Catalog { get; }
}