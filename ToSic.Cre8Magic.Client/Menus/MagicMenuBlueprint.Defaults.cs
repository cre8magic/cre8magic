using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Menu Design Settings
/// </summary>
public partial record MagicMenuBlueprint
{
    internal static Defaults<MagicMenuBlueprint> Defaults = new()
    {
        //// The default/fallback design settings for menus.
        //// Normally this would be set in the json file or the theme settings, so this wouldn't be used. 
        //Fallback = new MagicMenuBlueprintBs5(),
        
        // WIP 2024-01-09 2dm - should have an empty blueprint as fallback...
        Fallback = new(),
    };
}