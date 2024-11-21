namespace ToSic.Cre8magic.Settings.Internal.Docs;

/// <summary>
/// Internal interface to specify settings which extend the ...Data settings.
/// They will usually have things such as `PartName`.
/// </summary>
internal interface ISettingsWithNames
{
    /// <summary>
    /// Name to identify this part.
    /// This information is used to load settings (menu settings and design settings)
    /// </summary>
    public string? PartName { get; init; }
}