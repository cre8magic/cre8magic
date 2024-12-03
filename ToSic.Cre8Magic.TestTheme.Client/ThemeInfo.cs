using Oqtane.Models;
using Oqtane.Themes;

// Important: This must match the namespace of the layouts
// otherwise the Oqtane registration won't work as expected.
namespace ToSic.Cre8magic.TestTheme.Client;

/// <summary>
/// This class / file serves a few purposes:
///
/// 1. The theme-info for registration in Oqtane - because it inherits <see cref="ITheme"/>
/// 2. Contains names / constants like `MenuMobile` used in the theme
/// 2. Contains the default configurations/settings in the static `PackageDefaults`
/// </summary>
public class ThemeInfo : ITheme
{
    /// <summary>
    /// The standard information object for Oqtane to register the theme.
    /// </summary>
    public Theme Theme => new()
    {
        Name = "cre8magic Test Theme",
        Version = "0.3.1",

        // Package Name - used for NuGet and also for paths
        PackageName = "ToSic.Cre8magic.TestTheme",
        Dependencies = typeof(MagicThemeBase).AssemblyQualifiedName
    };

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
