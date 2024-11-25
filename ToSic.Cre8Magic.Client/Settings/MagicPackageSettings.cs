namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Settings for a Theme Package.
///
/// It contains things like
/// 
/// 1. location of assets
/// 1. settings for various parts like CSS
/// </summary>
public record MagicPackageSettings
{
    /// <summary>
    /// All kinds of settings for the layout, how it should be etc.
    /// Should usually only serve as backup in case the JSON fails.
    /// </summary>
    public MagicSettingsCatalog? Defaults { get; init; }

    /// <summary>
    /// WIP
    /// </summary>
    public MagicSettingsCatalog? Catalog { get; init; }

    public string WwwRoot { get; init; } = "wwwroot";

    public string SettingsJsonFile { get; init; } = "theme.json";

    public string PackageName { get; init; } = "todo: set theme package name in your constructor";

    public string Url
    {
        get => _url ??= "Themes/" + PackageName;
        init => _url = value;
    }
    private string? _url;

    /// <summary>
    /// `IsConfigured` is for internal use only.
    /// Basically every instance of this will report true,
    /// unless our default / fallback instance is used,
    /// which will set this to false.
    /// </summary>
    internal bool IsConfigured { get; init; } = true;

    internal static MagicPackageSettings Fallback = new()
    {
        Defaults = MagicSettingsCatalog.Fallback,
        WwwRoot = "wwwroot",
        SettingsJsonFile = "",
        PackageName = "Fallback-Not-Configured",
        IsConfigured = false,
    };
}