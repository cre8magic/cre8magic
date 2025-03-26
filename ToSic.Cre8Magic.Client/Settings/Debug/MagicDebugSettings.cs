using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Debug;

/// <summary>
/// Debug settings to help during development.
///
/// These settings can also be loaded from the configuration.
/// This allows you to do things like enable temporarily for admins, without restarting/recompiling anything.
/// </summary>
public record MagicDebugSettings
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
    }

    private MagicDebugSettings(MagicDebugSettings? priority, MagicDebugSettings? fallbackOriginal = default)
    {
        // Allowed is special, as it should only come from the master / fallback
        Allowed = fallbackOriginal?.Allowed ?? priority?.Allowed ?? false;
        Anonymous = priority?.Anonymous ?? fallbackOriginal?.Anonymous;
        Admin = priority?.Admin ?? fallbackOriginal?.Admin;
    }

    // TODO: make internal or something
    [PrivateApi]
    public MagicDebugState GetState(object? target, bool isAdmin)
        => (target is not IHasDebugSettings debugTarget ? this : new(debugTarget.Debug, this))
            .GetState(isAdmin);

    #endregion


    public bool? Allowed { get; init; }

    public bool? Anonymous { get; init; }

    public bool? Admin { get; init; }

    public bool? Detailed { get; init; }

    internal MagicDebugState GetState(bool isAdmin)
    {
        var stable = GetStable();
        return stable.Allowed
            ? new()
            {
                Show = isAdmin
                    ? stable.Admin      // Use configuration for Admin, by default admins see the debug
                    : stable.Anonymous  // Use configuration for Anonymous, by default anonymous users don't see the debug
            }
            : new();
    }

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
    public record Stabilized(MagicDebugSettings? DebugSettings)
    {
        public bool Allowed
        {
            get => _allowed ??= DebugSettings?.Allowed ?? false;
            init => _allowed = value;
        }
        private bool? _allowed;

        public bool Anonymous
        {
            get => _anonymous ??= DebugSettings?.Anonymous ?? false;
            init => _anonymous = value;
        }

        private bool? _anonymous;

        public bool Admin
        {
            get => _admin ??= DebugSettings?.Admin ?? true;
            init => _admin = value;
        }
        private bool? _admin;

        public bool Detailed
        {
            get => _detailed ??= DebugSettings?.Detailed ?? false;
            init => _detailed = value;
        }
        private bool? _detailed;
    }

    #endregion
}