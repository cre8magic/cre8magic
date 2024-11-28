using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

/// <summary>
/// Temporary design object to add to the Kits.
///
/// Should be refactored afterward, to not need the page list but something better
/// </summary>
/// <param name="page"></param>
internal class PageListDesignWip(IMagicPage page) : IMagicDesign
{
    public string? Classes(string tag) => page.Classes(tag);

    public string? Value(string tagOrKey) => page.Value(tagOrKey);
}