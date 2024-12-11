using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Journal;

namespace ToSic.Cre8magic.Themes.Internal;

/// <summary>
/// Lightweight context, mainly used for retrieving settings parts.
/// </summary>
/// <param name="SettingsName"></param>
/// <param name="ThemeSpell"></param>
/// <param name="Journal"></param>
public record CmThemeContext(
    string SettingsName,
    MagicThemeSpell ThemeSpell,
    Journal Journal
)
{
    [field: AllowNull, MaybeNull]
    internal ThemePartNameResolver NameResolver => field ??= new(this);
}