using Oqtane.UI;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Themes.Internal;

public record CmThemeContextFull : CmThemeContext
{
    public required PageState PageState { get; init; }
    public required MagicThemeBlueprint ThemeBlueprint { get; init; }
    public required TokenEngine PageTokens { get; init; }
}