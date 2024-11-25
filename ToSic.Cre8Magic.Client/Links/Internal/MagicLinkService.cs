using Oqtane.Shared;
using Oqtane.UI;

namespace ToSic.Cre8magic.Links;

internal class MagicLinkService : IMagicLinkService
{
    public string Link(PageState pageState, MagicLinkSpecs linkSpecs)
    {
        return Utilities.NavigateUrl(pageState.Alias.Path, linkSpecs.Path ?? pageState.Page.Path, linkSpecs.QueryString);
    }
}