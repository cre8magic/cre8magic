using Oqtane.UI;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// The execution context - WIP.
/// Goal is that it contains logging etc. which is shared across all parts which are getting something done.
/// </summary>
internal record ContextWip<TSettings, TDesigner> : IContextWip
{
    public required MagicPageFactory PageFactory { get; init; }

    public required TSettings Settings { get; init; }

    // TODO: ATM still nullable, should be changed
    public required TDesigner? Designer { get; init; }

    public IMagicPageDesigner? PageDesigner => Designer as IMagicPageDesigner;

    internal required LogRoot LogRoot { get; init; }

    LogRoot IContextWip.LogRoot => LogRoot;

    public required TokenEngine? TokenEngineWip { get; init; }
}