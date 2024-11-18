using Oqtane.UI;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Settings;

public class DesignerContextWip(
    MagicAllSettings allSettings,
    PageState pageState,
    LogRoot? logRoot = default)
{
    public PageState PageState { get; } = pageState;

    internal LogRoot LogRoot { get; } = logRoot ?? new();

    internal TokenEngine TokenEngineWip => allSettings.Tokens;
}