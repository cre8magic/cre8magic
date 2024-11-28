using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Internal.Debug;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// tbd
/// </summary>
internal record MagicMenuKit : IMagicMenuKit
{
    public required IMagicPage Root { get; init; }

    public required MagicMenuSettings Settings { get; init; }

    // TODO:
    //public object Designer => Pages.Settings.Designer;

    /// <summary>
    /// The variant - never null; defaults to ...
    /// </summary>
    public string Variant => Settings.Variant ?? "";

    public bool IsVariant(string variant) =>
        Variant.Equals(variant, StringComparison.OrdinalIgnoreCase);

    public /* actually internal */ required WorkContext WorkContext { get; init; }

    public DebugInfo GetDebugInfo() => new()
    {
        Title = "Settings (Menu)",
        More = new()
        {
            { "Settings", Settings },
            { "Design Settings", Settings.DesignSettings },
            { "Log", this.GetLogEntries() },
        },
        Settings = Settings,
        Values = new()
        {
            //{ "Show", Show },
            { "Part Name", DebugInfo.ShowNotSet(Settings.PartName) },
            { "Settings Name", DebugInfo.ShowNotSet(Settings.SettingsName) },
            { "Design Name", DebugInfo.ShowNotSet(Settings.DesignName) },
            { "Variant", DebugInfo.ShowNotSet(Variant) },
        }
    };
}