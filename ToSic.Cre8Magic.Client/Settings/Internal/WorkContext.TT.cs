using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// The execution context - WIP.
/// Goal is that it contains logging etc. which is shared across all parts which are getting something done.
/// </summary>
internal record WorkContext<TSettings, TDesigner> : WorkContext
{
    //public required MagicPageFactory PageFactory { get; init; }

    public required TSettings Settings { get; init; }

    // TODO: ATM still nullable, should be changed
    public required TDesigner? Designer { get; init; }

    //public required LogRoot LogRoot { get; init; }

    //public required TokenEngine? TokenEngine
    //{
    //    get => field ?? new();
    //    init;
    //}
}