using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Internal.Debug;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Themes.Internal;

internal record MagicThemeKit : IMagicThemeKit
{
    internal required CmThemeContextFull Context { get; init; }

    public MagicThemeSettings Settings => Context.ThemeSettings;

    public MagicThemeDesignSettings DesignSettings => Context.ThemeDesignSettings;

    public required MagicThemeDesigner Designer { get; init; }

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
                { "Theme Design", DesignSettings },
            },
            Settings = Settings,
            Values = new()
            {
                //{ "Part Name", DebugInfo.ShowNotSet(Settings.PartName) },
                //{ "Settings Name", DebugInfo.ShowNotSet(Settings.SettingsName) },
                //{ "Design Name", DebugInfo.ShowNotSet(Settings.DesignName) },
                { "Logo", DebugInfo.ShowNotSet(Logo) },
            }
        };
        return debugInfo;

    }
}