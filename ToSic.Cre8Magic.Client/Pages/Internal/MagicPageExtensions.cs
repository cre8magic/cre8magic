namespace ToSic.Cre8magic.Pages.Internal;

internal static class MagicPageExtensions
{
    internal static string LogPageList(this List<IMagicPage>? pages) =>
        pages?.Any() == true ? $"{pages.Count} pages [" + string.Join(",", pages.Select(p => p.Id)) + "]" : "(no pages)";

    internal static List<IMagicPage> LevelPages(this IEnumerable<IMagicPage> pages, int level) =>
        pages.Where(p => p.MenuLevel == level).ToList();
}