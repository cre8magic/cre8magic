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
public class ContextWip<TSettings, TDesigner>(
    //MagicAllSettings? allSettings,
    TSettings settings,
    TDesigner? designer,
    PageState pageState /* probably replace with a service? */,
    MagicPageFactory pageFactory,
    TokenEngine pageTokens,
    LogRoot? logRoot = default) : IContextWip
{
    public PageState PageState { get; } = pageState;

    public MagicPageFactory PageFactory { get; } = pageFactory;

    public TSettings Settings { get; } = settings;

    // TODO: ATM still nullable, should be changed
    public TDesigner? Designer { get; } = designer;

    public IMagicPageDesigner? PageDesigner => Designer as IMagicPageDesigner;

    internal LogRoot LogRoot { get; } = logRoot ?? new();

    LogRoot IContextWip.LogRoot => LogRoot;

    TokenEngine? IContextWip.TokenEngineWip => pageTokens;
}