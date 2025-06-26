using ToSic.Cre8magic.Internal.Debug;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes;

public interface IMagicThemeKit: IHasDebugInfo, IHasPageState
{
    MagicThemeSettings Settings { get; }
    MagicThemeTailor Tailor { get; }
    string Logo { get; }

    /// <summary>
    /// Determine if we should show a specific part
    /// </summary>
    bool ShowPart(string name);
}