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
    /// All kinds of settings for the layout, how it should be etc.
    /// Should usually only serve as backup in case the JSON fails.
    /// </summary>
    public MagicSettingsCatalog? Defaults { get; init; }

    public string WwwRoot { get; init; } = "wwwroot";

    public string SettingsJsonFile { get; init; } = "theme.json";

    public string PackageName { get; init; } = "todo: set theme package name in your constructor";

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