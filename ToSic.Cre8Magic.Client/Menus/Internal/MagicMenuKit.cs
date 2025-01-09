using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Internal.Debug;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// tbd
/// </summary>
internal record MagicMenuKit : IMagicMenuKit
{
    public required IMagicPage Root { get; init; }

    public required MagicMenuSpell Spell { get; init; }

    // TODO:
    //public object Designer => Pages.Settings.Designer;

    /// <summary>
    /// The variant - never null; defaults to ...
    /// </summary>
    public string Variant => Spell.Variant ?? "";

    public bool IsVariant(string variant) =>
        Variant.Equals(variant, StringComparison.OrdinalIgnoreCase);

    public /* actually internal */ required WorkContext WorkContext { get; init; }

    public DebugInfo GetDebugInfo() => new()
    {
        Title = "Settings (Menu)",
        More = new()
        {
            { "Settings", Spell },
            { "Design Settings", Spell.Blueprint },
            { "Log", this.GetLogEntries() },
        },
        Settings = Spell,
        Values = new()
        {
            //{ "Show", Show },
            { "Name", DebugInfo.ShowNotSet(Spell.Name) },
            { "Blueprint Name", DebugInfo.ShowNotSet(Spell.BlueprintName) },
            { "Variant", DebugInfo.ShowNotSet(Variant) },
        }
    };
}