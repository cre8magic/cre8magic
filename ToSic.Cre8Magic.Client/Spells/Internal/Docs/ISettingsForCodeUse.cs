namespace ToSic.Cre8magic.Spells.Internal.Docs;

/// <summary>
/// Internal interface to specify settings which extend the ...Data settings.
/// They will usually have things such as `Name`.
/// </summary>
internal interface ISettingsForCodeUse
{
    /// <summary>
    /// Name to identify this part.
    /// This information is used to load settings (menu settings and design settings)
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Name to identify which settings to load.
    /// This is used before looking in the Theme Part.
    /// If not specified, will check the theme part for a name it provides, or use the theme-part name to find the settings.
    /// </summary>
    public string? SettingsName { get; init; }

    /// <summary>
    /// Name to identify which design settings to load.
    /// </summary>
    public string? DesignName { get; init; }
}