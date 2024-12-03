using System.Diagnostics.CodeAnalysis;
using Oqtane.Themes;
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
    public MagicThemePackage() { }

    /// <summary>
    /// Constructor to create a new theme package definition from an existing theme.
    /// </summary>
    /// <param name="themeInfo"></param>
    public MagicThemePackage(ITheme themeInfo)
    {
        // The package name is important, as it's used to find assets etc.
        PackageName = themeInfo.Theme.PackageName;

        // The json file in the theme folder containing all kinds of settings etc.
        SettingsJsonFile = "theme.json";
    }

    /// <summary>
    /// All kinds of settings for the layout, how it should be etc.
    /// Should usually only serve as backup in case the JSON fails.
    /// </summary>
    public MagicSettingsCatalog? Defaults { get; init; }

    public string WwwRoot { get; init; } = "wwwroot";

    public string SettingsJsonFile { get; init; } = "theme.json";

    public string PackageName { get; init; } = "todo: set theme package name in your constructor";

    [field: AllowNull, MaybeNull]
    public string ThemePath => field ??= "Themes/" + PackageName;

    [field: AllowNull, MaybeNull]
    public string Url
    {
        get => field ??= "Themes/" + PackageName;
        init => field = value;
    }

    internal static MagicThemePackage Fallback = new()
    {
        Defaults = MagicSettingsCatalog.Fallback,
        WwwRoot = "wwwroot",
        SettingsJsonFile = "",
        PackageName = "Fallback-Not-Configured",
    };
}