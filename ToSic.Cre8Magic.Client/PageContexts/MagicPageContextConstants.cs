namespace ToSic.Cre8magic.PageContexts;

[PrivateApi]
public class MagicPageContextConstants
{
    public const string SitePrefixDefault = "site";
    public const string PagePrefixDefault = "page";
    public const string ThemePrefix = "theme";
    public const string MenuLevelPrefixDefault = "nav-level";

    public static string[] RecommendedClassList =
    [
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

        //5.1 Set the theme-variant- class
        $"{ThemePrefix}-variant-{MagicTokens.LayoutVariation}",

        // TODO: THIS IS PROBABLY not the right place for menu-placement
        //5.2 Set the theme-mainnav-variant- class to align the menu
        $"{ThemePrefix}-mainnav-variant-right",
    ];

    public const string RecommendedPageIsHome = $"{PagePrefixDefault}-is-home";
}