using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Themes.Settings;

public partial record MagicThemeDesignSettings
{
    private const string SitePrefixDefault = "site";
    private const string PagePrefixDefault = "page";
    internal const string MainPrefix = "theme";
    private const string PanePrefixDefault = "pane";
    private const string MenuLevelPrefixDefault = "nav-level";
    private const string BodyDivId = "cre8magic-root";
    internal const string SettingFromDefaults = $"{MainPrefix}-warning-this-is-from-defaults-you-should-set-your-own-value";

    private const string ContainerIdDefault = "module-[Module.Id]";
    internal const string ModulePrefixDefault = "module";


    internal static Defaults<MagicThemeDesignSettings> Defaults = new()
    {
        Fallback = new()
        {
            MagicContext = [
                //1.2 Set the page-### class
                $"{PagePrefixDefault}-{MagicTokens.PageId}",
                //1.4 Set the page-root-### class
                $"{PagePrefixDefault}-root-{MagicTokens.PageRootId}",
                //1.3 Set the page-parent-### class
                $"{PagePrefixDefault}-parent-{MagicTokens.PageParentId}",
                //2 Set site-### class
                $"{SitePrefixDefault}-{MagicTokens.SiteId}",
                //3 Set the nav-level-### class
                $"{MenuLevelPrefixDefault}-{MagicTokens.MenuLevel}",
                //5.1 Set the theme-variation- class
                $"{MainPrefix}-variation-{MagicTokens.LayoutVariation}",

                //5.2 Set the theme-mainnav-variation- class to align the menu
                $"{MainPrefix}-mainnav-variation-right",

                // Debug info so we know the defaults were used
                SettingFromDefaults
            ],
            PageIsHome = new($"{PagePrefixDefault}-is-home"),
            PaneIsEmpty = new($"{PanePrefixDefault}-is-empty"),
            MagicContextTagId = BodyDivId,
            Data = new()
            {
                // TODO: remove this, as we've replaced it in the language settings
                { "languages-li", new() { IsActive = new($"active {SettingFromDefaults}") } },
                {
                    "container", new()
                    {
                        Classes = $"{MainPrefix}-page-language {SettingFromDefaults}",
                        IsPublished = new(null,
                            $"{ModulePrefixDefault}-unpublished  {SettingFromDefaults}"),
                        IsAdmin = new($"{MainPrefix}-admin-container  {SettingFromDefaults}"),
                        Id = ContainerIdDefault,
                    }
                },
            },
        },
        Foundation = new()
        {
            MagicContext = [],
            PageIsHome = new(),
            PaneIsEmpty = new(),
            MagicContextTagId = BodyDivId,
            Data = new()
            {
                { "container", new() { Id = ContainerIdDefault } },
            }
        },
    };
}