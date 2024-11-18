using Oqtane.Models;
using Oqtane.UI;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// TODO: WIP - later make private with interface
/// </summary>
internal class MagicSettingsContext : IMagicSettingsContext
{
    /// <summary>
    /// Name of the settings - in case there are named settings to retrieve.
    ///
    /// Can be null, for example when the default settings are requested.
    /// </summary>
    public string? Name { get; init; }

    public string Prefix { get; init; }

    public bool FallbackToDefault { get; init; } = true;

    /// <summary>
    /// The Page State, which could be used to deliver alternate settings depending on the page.
    /// </summary>
    public PageState PageState { get; init; }

    /// <summary>
    /// The Module State, which could be used to deliver alternate settings depending on the module.
    ///
    /// Can be null, if the settings are not related to a module.
    /// For example, Login Settings.
    /// </summary>
    public Module? ModuleState { get; init; }
}