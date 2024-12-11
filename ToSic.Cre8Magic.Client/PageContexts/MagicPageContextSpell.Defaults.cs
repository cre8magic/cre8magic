using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.PageContexts;

/// <summary>
/// Magic Page Context Settings - Data.
///
/// This configures how the page context is rendered, and what classes are added to the body tag.
/// </summary>
public partial record MagicPageContextSpell
{
    private const string SitePrefixDefault = "site";
    private const string PagePrefixDefault = "page";
    internal const string MainPrefix = "theme";
    private const string MenuLevelPrefixDefault = "nav-level";
    private const string BodyDivId = "cre8magic-root";
    internal const string SettingFromDefaults = $"{MainPrefix}-warning-this-is-from-defaults-you-should-set-your-own-value";

    internal static Defaults<MagicPageContextSpell> Defaults = new()
    {
        Foundation = new()
        {
            // MagicContext = [],
            PageIsHome = new(),
            TagId = BodyDivId,
        },
        Fallback = new()
        {
            ClassList = [
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
                // TODO: VARIANT instead of variation
                //5.1 Set the theme-variation- class
                $"{MainPrefix}-variation-{MagicTokens.LayoutVariation}",

                //5.2 Set the theme-mainnav-variation- class to align the menu
                $"{MainPrefix}-mainnav-variation-right",

                // Debug info so we know the defaults were used
                SettingFromDefaults
            ],
            PageIsHome = new($"{PagePrefixDefault}-is-home"),
            TagId = BodyDivId,
        }
    };
}