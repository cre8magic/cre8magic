using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Users;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Debug;

/// <summary>
/// Debug settings to help during development.
///
/// These settings can also be loaded from the configuration.
/// This allows you to do things like enable temporarily for admins, without restarting/recompiling anything.
/// </summary>
public record MagicDebugSettings : ICanClone<MagicDebugSettings>
{
    #region Constructors and Cloning

    /// <summary>
    /// For JSON deserialization
    /// </summary>
    [PrivateApi]
    public MagicDebugSettings() { }

    /// <summary>
    /// Create debug settings which set everything to the specified state.
    /// This is mainly for quick interventions, so you can just do `new(true)` to enable everything.
    /// </summary>
    /// <param name="enable"></param>
    public MagicDebugSettings(bool enable)
    {
        Allowed = enable;
        Anonymous = enable;
        Admin = enable;
        Editor = enable;
    }

    /// <summary>
    /// Clone/Merge constructor - internal use only.
    /// </summary>
    internal MagicDebugSettings(MagicDebugSettings? priority, MagicDebugSettings? fallback = default)
    {
        Allowed = priority?.Allowed ?? fallback?.Allowed;
        Anonymous = priority?.Anonymous ?? fallback?.Anonymous;
        Admin = priority?.Admin ?? fallback?.Admin;
        Editor = priority?.Editor ?? fallback?.Editor;
    }

    MagicDebugSettings ICanClone<MagicDebugSettings>.CloneUnder(MagicDebugSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion

    /// <summary>
    /// Specify if debug is allowed at all.
    /// </summary>
    public bool? Allowed { get; init; }

    /// <summary>
    /// Specify if debug should be activated for anonymous.
    /// Mainly for development to see differences between logged in and out.
    /// </summary>
    public bool? Anonymous { get; init; }

    /// <summary>
    /// Specify if debug should be activated for admins.
    /// </summary>
    public bool? Admin { get; init; }

    /// <summary>
    /// Specify if debug should be activated for admins.
    /// </summary>
    public bool? Editor { get; init; }

    /// <summary>
    /// Determine if detailed debug information should be shown.
    /// </summary>
    public bool? Detailed { get; init; }


    #region Stabilized

    [PrivateApi]
    public Stabilized GetStable() => (_stabilized ??= new(new(this))).Value;
    private IgnoreEquals<Stabilized>? _stabilized;

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public record Stabilized(MagicDebugSettings DebugSettings)
    {
        public bool Allowed => DebugSettings.Allowed ?? false;

        public bool Anonymous => DebugSettings.Anonymous ?? false;

        public bool Admin => DebugSettings.Admin ?? true;

        public bool Editor => DebugSettings.Editor ?? false;

        public bool Detailed => DebugSettings.Detailed ?? false;

        #region Derived / calculated properties

        /// <summary>
        /// Determine if debug should be shown.
        /// The result will vary depending on if the user is an admin or not.
        /// </summary>
        /// <returns></returns>
        public bool Show(PageState pageState) => Allowed
                                                 && (pageState.UserIsAdmin()
                                                     ? Admin
                                                     : pageState.UserMayEditCurrentPage()
                                                         ? Editor
                                                         : Anonymous);

        #endregion
    }

    #endregion
}