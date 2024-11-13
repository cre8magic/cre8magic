
namespace ToSic.Cre8magic.Pages;

/// <summary>
/// WIP
/// TODO: naming not final
/// </summary>
public interface IMagicPageSetSettings
{
    /// <summary>
    /// Id of the settings, usually a random number
    ///
    /// TODO: UNCLEAR difference to MenuId - or could be the same?
    /// </summary>
    string? Id { get; }

    /// <summary>
    /// Menu ID for use in JavaScript etc.
    /// Usually predefined OR a random number
    /// </summary>
    string MenuId { get; }

    string? Variant { get; }
}