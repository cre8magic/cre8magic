using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Menu Design Settings
/// </summary>
public partial record MagicMenuBlueprint
{
    internal static Defaults<MagicMenuBlueprint> Defaults = new()
    {
        // The default/fallback design settings for menus.
        // Normally this would be set in the json file or the theme settings, so this wouldn't be used. 
        Fallback = new()
        {
            Parts = new(StringComparer.InvariantCultureIgnoreCase)
            {
                {
                    "a", new()
                    {
                        IsActive = new("active"),
                        HasChildren = new("dropdown-toggle"),
                        ByLevel = new()
                        {
                            { MagicTokens.ByLevelOtherKey, "dropdown-item" },
                            { 1, "nav-link" },

                        }
                    }
                },
                {
                    "li", new()
                    {
                        Classes = $"nav-item nav-{MagicTokens.PageId}",
                        HasChildren = new("has-child dropdown"),
                        IsActive = new("active"),
                        IsDisabled = new("disabled"),
                    }

                },
                {
                    "ul", new()
                    {
                        ByLevel = new()
                        {
                            { MagicTokens.ByLevelOtherKey, "dropdown-menu" },
                            { 0, "navbar-nav" },
                        },
                    }
                }
            },
        },
    };
}