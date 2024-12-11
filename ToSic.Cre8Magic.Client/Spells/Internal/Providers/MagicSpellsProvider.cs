using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Internal.Providers;

internal class MagicSpellsProvider: IMagicSpellsProvider, IMagicSpellsBooksSource
{
    int IMagicSpellsBooksSource.Priority => 200;

    /// <summary>
    /// Implement interface to get this stuff
    /// </summary>
    /// <param name="themePackage"></param>
    /// <returns></returns>
    public List<DataWithJournal<MagicSpellsBook>> SpellsBooks(MagicThemePackage themePackage)
    {
        var partsNoData = AllSources.All(source => source?.HasValues != true);
        if (partsNoData)
            return _book == null
                ? []
                : [new(_book, new())];

        var book = _book ?? new MagicSpellsBook();

        if (!partsNoData)
            book = book with
            {
                Analytics = _analytics?.Values != null ? new(_analytics.Values) : book.Analytics,
                Breadcrumbs = _breadcrumbs?.Values != null ? new(_breadcrumbs.Values) : book.Breadcrumbs,
                Containers = _containers?.Values != null ? new(_containers.Values) : book.Containers,
                MenuBlueprints = _menuDesigns?.Values != null
                    ? new(_menuDesigns.Values.ToDictionary(
                        dic => dic.Key,
                        dic => dic.Value
                    ))
                    : book.MenuBlueprints,
                Themes = _themes?.Values != null ? new(_themes.Values) : book.Themes,
            };

        return [new(book, new())];
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

    public IMagicSpellsProvider Provide(MagicSpellsBook book)
    {
        _book = book;
        return this;
    }

    public MagicSpellsBook? Book => SpellsBooks(null!).FirstOrDefault()?.Data;

    private MagicSpellsBook? _book;

    public IMagicSpellsProviderSection<MagicAnalyticsSpell> Analytics => _analytics ??= new(this);
    private MagicSpellsProviderSection<MagicAnalyticsSpell>? _analytics;

    public IMagicSpellsProviderSection<MagicBreadcrumbSpell> Breadcrumbs => _breadcrumbs ??= new(this);
    private MagicSpellsProviderSection<MagicBreadcrumbSpell>? _breadcrumbs;

    public IMagicSpellsProviderSection<MagicContainerSpell> Containers => _containers ??= new(this);
    private MagicSpellsProviderSection<MagicContainerSpell>? _containers;

    public IMagicSpellsProviderSection<MagicMenuBlueprint> MenuBlueprints => _menuDesigns ??= new(this);
    private MagicSpellsProviderSection<MagicMenuBlueprint>? _menuDesigns;

    public IMagicSpellsProviderSection<MagicThemeSpell> Themes => _themes ??= new(this);
    private MagicSpellsProviderSection<MagicThemeSpell>? _themes;
}