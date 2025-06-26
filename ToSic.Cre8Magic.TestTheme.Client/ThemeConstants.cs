namespace ToSic.Cre8magic.TestTheme.Client;

internal class ThemeConstants
{
    #region Menu Names for this Theme, used in various Razor files

    public const string MenuMain = "main";
    public const string MenuMobile = "mobile";
    public const string MenuSidebar = "sidebar";
    public const string MenuFooter = "footer";

    #endregion

    #region Constants for generating CSS Classes

    // Note: not "theme-" because we need the "-" as separator in razor files it must be @ThemeInfo.ClassPrefix-something-else
    public const string ClassPrefix = "theme";

    #endregion
}