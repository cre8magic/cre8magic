using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Things needed to do work together.
/// For example, the PageFactory could be pre-configured.
/// </summary>
internal record WorkContext
{
    public required LogRoot LogRoot { get; init; }

    public required MagicPageFactory PageFactory { get; init; }

    public required TokenEngine TokenEngine { get; init; }
}