using Oqtane.UI;

namespace ToSic.Cre8magic.Links;

public interface IMagicLinkService
{
    string Link(PageState pageState, MagicLinkSettings settings);
}