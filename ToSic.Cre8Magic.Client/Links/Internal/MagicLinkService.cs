using Oqtane.Shared;
using Oqtane.UI;

namespace ToSic.Cre8magic.Links.Internal;

internal class MagicLinkService : IMagicLinkService
{
    public string Link(PageState pageState, MagicLinkSettings settings)
    {
        return Utilities.NavigateUrl(pageState.Alias.Path, settings.Path ?? pageState.Page.Path, settings.QueryString);
    }
}