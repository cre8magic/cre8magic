using Oqtane.Models;
using Oqtane.UI;

namespace ToSic.Cre8magic.Settings;

public interface IMagicSettingsContext
{
    /// <summary>
    /// Name of the settings - in case there are named settings to retrieve.
    ///
    /// Can be null, for example when the default settings are requested.
    /// </summary>
    string? Name { get; }

    bool FallbackToDefault { get; }

    string Prefix { get; }

    /// <summary>
    /// The Page State, which could be used to deliver alternate settings depending on the page.
    /// </summary>
    PageState PageState { get; }

    /// <summary>
    /// The Module State, which could be used to deliver alternate settings depending on the module.
    ///
    /// Can be null, if the settings are not related to a module.
    /// For example, Login Settings.
    /// </summary>
    Module? ModuleState { get; }
}