using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Pages;

internal static class MagicPageExtensions
{
    internal static string LogPageList(this List<MagicPage>? pages) =>
        pages?.Any() == true ? $"{pages.Count} pages [" + string.Join(",", pages.Select(p => p.PageId)) + "]" : "(no pages)";
}