using Oqtane.UI;

namespace ToSic.Cre8magic.Links;

public interface IMagicLinkService
{
    /// <summary>
    /// Get a link based on the settings.
    /// </summary>
    /// <param name="pageState"></param>
    /// <param name="settings">Settings to specify what link; if not provided, will currently return the link to the current page, but this may change.</param>
    /// <returns></returns>
    string Link(PageState pageState, MagicLinkSettings? settings = default);
}