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

}