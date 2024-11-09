using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages;

internal static class MagicPageExtensions
{
    internal static string LogPageList(this List<IMagicPage>? pages) =>
        pages?.Any() == true ? $"{pages.Count} pages [" + string.Join(",", pages.Select(p => p.PageId)) + "]" : "(no pages)";
}