using ToSic.Cre8magic.Internal.Journal;
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
        if (_book != null && BookFromChapters != null)
        {
            var book = Merge([_book, BookFromChapters]);
            return [new(book, new())];

        }
        return BookFromChapters != null
            ? [new(BookFromChapters, new())]
            : _book != null
                ? [new(_book, new())]
                : [];
    }

    public void Reset()
    {
        BookFromChapters = null;
        Chapters.Clear();
    }

    public IMagicSettingsProvider Provide(MagicBook book)
    {
        _book = book;
        return this;
    }

    private MagicBook? _book;

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

        return new()
        {
            Source = "Provided",
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