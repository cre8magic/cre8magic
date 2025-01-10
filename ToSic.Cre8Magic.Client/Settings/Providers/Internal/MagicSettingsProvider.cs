using System.Dynamic;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.Settings.Providers.Internal;

internal class MagicSettingsProvider: IMagicSettingsProvider, IMagicBooksSource
{
    int IMagicBooksSource.Priority => 200;

    /// <summary>
    /// Implement interface to get this stuff
    /// </summary>
    /// <param name="themePackage"></param>
    /// <returns></returns>
    public List<DataWithJournal<MagicBook>> Books(MagicThemePackage themePackage)
    {
        var partsNoData = AllSources.All(source => source?.HasValues != true);
        if (partsNoData)
            return BookFromChapters != null
                ? [new(BookFromChapters, new())]
                : _book == null
                    ? []
                    : [new(_book, new())];

        var book = _book ?? new MagicBook();

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

        if (BookFromChapters != null)
            book = Merge([book, BookFromChapters]);

        return [new(book, new())];
    }

    public void Reset()
    {
        foreach (var source in AllSources) 
            source?.Reset();
        BookFromChapters = null;
        Chapters.Clear();
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

    public IMagicSettingsProvider Provide(MagicBook book)
    {
        _book = book;
        return this;
    }

    public MagicBook? Book => Books(null!).FirstOrDefault()?.Data;

    private MagicBook? _book;

    public IMagicSettingsProviderSection<MagicAnalyticsSettings> Analytics => _analytics ??= new(this);
    private MagicSettingsProviderSection<MagicAnalyticsSettings>? _analytics;

    public IMagicSettingsProviderSection<MagicBreadcrumbSettings> Breadcrumbs => _breadcrumbs ??= new(this);
    private MagicSettingsProviderSection<MagicBreadcrumbSettings>? _breadcrumbs;

    public IMagicSettingsProviderSection<MagicContainerSettings> Containers => _containers ??= new(this);
    private MagicSettingsProviderSection<MagicContainerSettings>? _containers;

    public IMagicSettingsProviderSection<MagicMenuBlueprint> MenuBlueprints => _menuDesigns ??= new(this);
    private MagicSettingsProviderSection<MagicMenuBlueprint>? _menuDesigns;

    public IMagicSettingsProviderSection<MagicThemeSettings> Themes => _themes ??= new(this);
    private MagicSettingsProviderSection<MagicThemeSettings>? _themes;

    #region WIP try to switch to chapters for simplicity

    private List<MagicChapter> Chapters { get; }= [];

    private MagicBook? BookFromChapters
    {
        get => field ??= Chapters.Any() ? new MagicBook { Chapters = Chapters } : null;
        set => field = value;
    }

    public void Add(MagicChapter chapter)
    {
        Chapters.Add(chapter);
        BookFromChapters = null;
    }

    private MagicBook Merge(IEnumerable<MagicBook> books)
    {
        var list = books.ToList();
        var themes = Merge(list.Select(l => l.Themes));
        var themeBlueprints = Merge(list.Select(l => l.ThemeBlueprints));
        var analytics = Merge(list.Select(l => l.Analytics));
        var breadcrumbs = Merge(list.Select(l => l.Breadcrumbs));
        var breadcrumbBlueprints = Merge(list.Select(l => l.BreadcrumbBlueprints));
        var containers = Merge(list.Select(l => l.Containers));
        var containerBlueprints = Merge(list.Select(l => l.ContainerBlueprints));
        var languages = Merge(list.Select(l => l.Languages));
        var languageBlueprints = Merge(list.Select(l => l.LanguageBlueprints));
        var menus = Merge(list.Select(l => l.Menus));
        var menuBlueprints = Merge(list.Select(l => l.MenuBlueprints));
        var pageContexts = Merge(list.Select(l => l.PageContexts));

        return new MagicBook
        {
            Themes = themes,
            ThemeBlueprints = themeBlueprints,
            Analytics = analytics,
            Breadcrumbs = breadcrumbs,
            BreadcrumbBlueprints = breadcrumbBlueprints,
            Containers = containers,
            ContainerBlueprints = containerBlueprints,
            Languages = languages,
            LanguageBlueprints = languageBlueprints,
            Menus = menus,
            MenuBlueprints = menuBlueprints,
            PageContexts = pageContexts,
        };
    }

    private Dictionary<string, TValue> Merge<TValue>(IEnumerable<IDictionary<string, TValue>> dictionaries)
    {
        var result = dictionaries
            .SelectMany(dict => dict)
            .ToLookup(pair => pair.Key.ToLowerInvariant(), pair => pair.Value)
            .ToDictionary(group => group.Key, group => group.Last(), StringComparer.InvariantCultureIgnoreCase);
        return result;
    }

    #endregion
}