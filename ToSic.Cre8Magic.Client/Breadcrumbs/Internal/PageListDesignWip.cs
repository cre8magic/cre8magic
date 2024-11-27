using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

/// <summary>
/// Temporary design object to add to the Kits.
///
/// Should be refactored afterward, to not need the page list but something better
/// </summary>
/// <param name="pageListWip"></param>
internal class PageListDesignWip(IMagicPageList pageListWip) : IMagicDesign
{
    public string? Classes(string tag) => pageListWip.Classes(tag);

    public string? Value(string key) => pageListWip.Value(key);
}