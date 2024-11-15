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
    MagicAllSettings? allSettings,
    TSettings settings,
    TDesigner? designer,
    PageState pageState /* probably replace with a service? */,
    List<string>? debugMessages,
    MagicPageFactory? pageFactory) : IContextWip
{
    public PageState PageState { get; } = pageState;

    public MagicPageFactory PageFactory { get; } = pageFactory ?? new(pageState);

    public MagicAllSettings? AllSettings { get; } = allSettings;

    public TSettings Settings { get; } = settings;

    // TODO: ATM still nullable, should be changed
    public TDesigner? Designer { get; } = designer;

    public IMagicPageDesigner? PageDesigner => Designer as IMagicPageDesigner;

    public List<string> DebugMessages { get; } = debugMessages ?? [];

    internal LogRoot LogRoot { get; } = new();

    LogRoot IContextWip.LogRoot => LogRoot;

    TokenEngine? IContextWip.TokenEngineWip => AllSettings?.Tokens;
}