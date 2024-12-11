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
public interface IMagicSpellsProvider
{
    IMagicSpellsProviderSection<MagicContainerSpell> Containers { get; }

    IMagicSpellsProviderSection<MagicBreadcrumbSpell> Breadcrumbs { get; }
    IMagicSpellsProviderSection<MagicAnalyticsSpell> Analytics { get; }
    IMagicSpellsProviderSection<MagicMenuBlueprint> MenuBlueprints { get; }
    IMagicSpellsProviderSection<MagicThemeSpell> Themes { get; }
    public void Reset();
    IMagicSpellsProvider Provide(MagicSpellsBook book);

    internal MagicSpellsBook? Book { get; }
}