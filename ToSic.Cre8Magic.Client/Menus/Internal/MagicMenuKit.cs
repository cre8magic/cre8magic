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
    public string Variant => Settings.GetStable().Variant ?? "";

    public bool IsVariant(string variant) =>
        Variant.Equals(variant, StringComparison.OrdinalIgnoreCase);

    public /* actually internal */ required WorkContext WorkContext { get; init; }

    public DebugInfo GetDebugInfo() => new()
    {
        Title = "Settings (Menu)",
        More = new()
        {
            { "Settings", Settings },
            { "Blueprint", Settings.Blueprint },
            { "Log", this.GetLogEntries() },
        },
        Settings = Settings,
        Values = new()
        {
            //{ "Show", Show },
            { "Name", DebugInfo.ShowNotSet(Settings.Name) },
            { "Variant", DebugInfo.ShowNotSet(Variant) },
        }
    };
}