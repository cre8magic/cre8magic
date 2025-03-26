using Oqtane.Themes;
using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Themes;

/// <summary>
/// Settings for a Theme Package.
///
/// It contains things like
/// 
/// 1. location of assets
/// 1. settings for various parts like CSS
/// </summary>
public record MagicThemePackage
{
    /// <summary>
    /// Default / empty constructor; set properties manually.
    /// </summary>
    internal MagicThemePackage() { }

    /// <summary>
    /// Constructor to create a new theme package definition from an existing theme.
    /// </summary>
    /// <param name="themeInfo"></param>
    public MagicThemePackage(ITheme themeInfo)
    {
        // The package name is important, as it's used to find assets etc.
        PackageName = themeInfo.Theme.PackageName;

        // The json file in the theme folder containing all kinds of settings etc.
        SettingsFile = "theme.json";
    }

    /// <summary>
    /// All kinds of settings for the layout, how it should be etc.
    /// Should usually only serve as backup in case the JSON fails.
    /// </summary>
    public MagicBook? Defaults { get; init; }

    /// <summary>
    /// Name of the theme as is found in the json file.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Root of the WWW files - usually `wwwroot` (default)
    /// </summary>
    public string WwwRoot { get; init; } = "wwwroot";

    /// <summary>
    /// Name of the settings file in the theme folder.
    /// Usually `theme.json` (default)
    /// </summary>
    public string SettingsFile { get; init; } = "";// "theme.json";

    /// <summary>
    /// The PackageName which is used for paths etc.
    /// </summary>
    public string PackageName { get; init; } = "todo: set theme package name in your constructor";

    // TODO: THIS SHOULD help get access to the proper theme path, but ATM it's not in use so it's not final/standardized
    [field: AllowNull, MaybeNull]
    public string ThemePath => field ??= "Themes/" + PackageName;

    /// <summary>
    /// Url to reference files in the theme package.
    /// </summary>
    /// <remarks>
    /// This property is only valid if the theme package was created the normal way with a `ITheme` object.
    /// </remarks>
    [field: AllowNull, MaybeNull]
    public string Url
    {
        get => field ??= "Themes/" + PackageName;
        init => field = value;
    }

    internal static MagicThemePackage Fallback = new()
    {
        Defaults = MagicBook.Fallback,
        WwwRoot = "wwwroot",
        SettingsFile = "",
        PackageName = "Fallback-Not-Configured",
    };

}