namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Settings for a Theme Package.
///
/// Contains semi-constants like location of assets and configuration for various parts like CSS.
/// </summary>
public class MagicPackageSettings
{
    /// <summary>
    /// All kinds of settings for the layout, how it should be etc.
    /// Should usually only serve as backup in case the JSON fails.
    /// </summary>
    public MagicSettingsCatalog? Defaults { get; set; }

    public string WwwRoot { get; set; } = "wwwroot";

    public string SettingsJsonFile { get; set; } = "theme.json";

    public string PackageName { get; set; } = "todo: set theme package name in your constructor";

    public string Url
    {
        get => _url ??= "Themes/" + PackageName;
        set => _url = value;
    }
    private string? _url;

    /// <summary>
    /// IsConfigured is for internal use only.
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