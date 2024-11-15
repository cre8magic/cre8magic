using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Settings;

public interface IContextWip
{
    MagicPageFactory PageFactory { get; }
    internal LogRoot LogRoot { get; }

    IMagicPageDesigner? PageDesigner { get; }

    internal TokenEngine? TokenEngineWip { get; }
}