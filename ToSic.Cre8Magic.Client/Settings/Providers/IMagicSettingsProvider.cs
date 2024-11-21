using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;

namespace ToSic.Cre8magic.Settings.Providers;

/// <summary>
/// Special provider to retrieve settings.
///
/// It is scoped, so anything added to it - typically in the Theme,
/// will be available in all other components.
/// </summary>
public interface IMagicSettingsProvider
{
    IMagicProviderSection<MagicContainerSettings> Containers { get; }

    IMagicProviderSection<MagicBreadcrumbSettings> Breadcrumbs { get; }
    IMagicProviderSection<MagicAnalyticsSettings> Analytics { get; }
    IMagicProviderSection<Dictionary<string, MagicMenuDesignSettings>> MenuDesigns { get; }
    IMagicProviderSection<MagicThemeSettings> Themes { get; }
    public void Reset();
}