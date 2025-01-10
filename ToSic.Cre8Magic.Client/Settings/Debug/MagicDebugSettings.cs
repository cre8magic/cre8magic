using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;

namespace ToSic.Cre8magic.Settings.Debug;

/// <summary>
/// Debug settings to help during development.
///
/// These settings can also be loaded from the configuration.
/// This allows you to do things like enable temporarily for admins, without restarting/recompiling anything.
/// </summary>
public record MagicDebugSettings
{
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
        Anonymous = Merge(priority?.Anonymous, fallbackOriginal?.Anonymous);
        Admin = Merge(priority?.Admin, fallbackOriginal?.Admin);
    }

    // TODO: make internal or something
    [PrivateApi]
    public MagicDebugState GetState(object? target, bool isAdmin)
        => (target is not IHasDebugSettings debugTarget ? this : new(debugTarget.Debug, this))
            .Parsed(isAdmin);

    public bool? Allowed { get; init; }
    internal bool AllowedSafe => Allowed ?? false;

    public bool? Anonymous { get; init; }
    internal bool AnonymousSafe => Anonymous ?? false;

    public bool? Admin { get; init; }
    internal bool AdminSafe => Admin ?? true;

    public bool? Detailed { get; init; }



    private bool Merge(bool? priority, bool? fallback) => priority == true || priority == null && fallback == true;

    internal MagicDebugState Parsed(bool isAdmin) =>
        AllowedSafe
            ? new()
            {
                Show = isAdmin ? AdminSafe : AnonymousSafe
            }
            : new();

    internal static Defaults<MagicDebugSettings> Defaults = new(new()
    {
        Allowed = false,
        Anonymous = false,
        Admin = false,
    });
}