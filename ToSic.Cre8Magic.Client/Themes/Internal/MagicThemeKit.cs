using System.Diagnostics.CodeAnalysis;
using Oqtane.UI;
using ToSic.Cre8magic.Internal.Debug;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Internal;

internal record MagicThemeKit : IMagicThemeKit, IHasPageState
{
    internal required CmThemeContextFull Context { get; init; }

    public MagicThemeSettings Settings => Context.ThemeSettings;

    public MagicThemeBlueprint Blueprint => Context.ThemeBlueprint;

    public required MagicThemeTailor Tailor { get; init; }

    [field: AllowNull, MaybeNull]
    public string Logo => field ??= Context.PageTokens.Parse(Settings.Logo) ?? "";

    /// <summary>
    /// Determine if we should show a specific part
    /// </summary>
    public bool ShowPart(string name) =>
        Settings.Parts.TryGetValue(name, out var partSettings) && partSettings.Show == true;

    public DebugInfo GetDebugInfo()
    {
        var debugInfo = new DebugInfo
        {
            Title = "Theme Settings (Debug)",
            More = new()
            {
                { "Theme Parts", Settings },
                { "Theme Design", Blueprint },
            },
            Settings = Settings,
            Values = new()
            {
                { "Logo", DebugInfo.ShowNotSet(Logo) },
            }
        };
        return debugInfo;

    }

    PageState IHasPageState.PageState => Settings.PageState!;
}