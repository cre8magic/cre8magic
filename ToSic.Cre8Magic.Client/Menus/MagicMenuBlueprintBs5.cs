namespace ToSic.Cre8magic.Menus
{
    public record MagicMenuBlueprintBs5: MagicMenuBlueprint
    {
        public MagicMenuBlueprintBs5()
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
            };
        }
    }
}
