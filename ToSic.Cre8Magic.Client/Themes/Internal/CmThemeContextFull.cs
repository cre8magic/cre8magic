using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;

namespace ToSic.Cre8magic.Themes.Internal;

public record CmThemeContextFull(
    string SettingsName,
    PageState PageState,
    MagicThemeSpell ThemeSpell,
    MagicThemeBlueprint ThemeBlueprint,
    TokenEngine PageTokens,
    Journal Journal
): CmThemeContext(SettingsName, ThemeSpell, Journal);