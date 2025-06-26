using ToSic.Cre8magic.Menus;

namespace ToSic.Cre8magic.OqtaneBs5;

public partial class DefaultsBs5
{
    public MagicMenuBlueprint MagicMenuBlueprint => _magicMenuBlueprint;

    private static readonly MagicMenuBlueprint _magicMenuBlueprint = new()
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
    };
}

//public record MagicMenuBlueprintBs5: MagicMenuBlueprint
//{
//    public MagicMenuBlueprintBs5()
//    {
//        Parts = new(StringComparer.InvariantCultureIgnoreCase)
//        {
//            {
//                "a", new()
//                {
//                    IsActive = new("active"),
//                    HasChildren = new("dropdown-toggle"),
//                    ByLevel = new()
//                    {
//                        { MagicTokens.ByLevelOtherKey, "dropdown-item" },
//                        { 1, "nav-link" },
//                    }
//                }
//            },
//            {
//                "li", new()
//                {
//                    Classes = $"nav-item nav-{MagicTokens.PageId}",
//                    HasChildren = new("has-child dropdown"),
//                    IsActive = new("active"),
//                    IsDisabled = new("disabled"),
//                }

//            },
//            {
//                "ul", new()
//                {
//                    ByLevel = new()
//                    {
//                        { MagicTokens.ByLevelOtherKey, "dropdown-menu" },
//                        { 0, "navbar-nav" },
//                    },
//                }
//            }
//        };
//    }
//}