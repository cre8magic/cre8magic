namespace ToSic.Cre8magic.Settings.Internal.Debug;

public record MagicDebugSettings
{
    public MagicDebugSettings() { }

    public MagicDebugSettings(MagicDebugSettings? priority, MagicDebugSettings? fallbackOriginal = default)
    {
        // Allowed is special, as it should only come from the master / fallback
        Allowed = fallbackOriginal?.Allowed ?? priority?.Allowed ?? false;
        Anonymous = Merge(priority?.Anonymous, fallbackOriginal?.Anonymous);
        Admin = Merge(priority?.Admin, fallbackOriginal?.Admin);
    }

    public MagicDebugState GetState(object? target, bool isAdmin)
        => (target is not IHasDebugSettings debugTarget ? this : new(debugTarget.Debug, this))
            .Parsed(isAdmin);

    public bool? Allowed { get; init; }
    private const bool AllowedDefault = false;
    public bool? Anonymous { get; init; }
    private const bool AnonymousDefault = false;
    public bool? Admin { get; init; }
    private const bool AdminDefault = true;

    public bool? Detailed { get; init; }
    private const bool DetailedDefault = false;


    // Mechanism before 2024-11-13 - keep for a while just to be safe
    //private MagicDebugSettings Merge(MagicDebugSettings? priority, MagicDebugSettings original) =>
    //    priority == null
    //        ? original
    //        : new()
    //        {
    //            Allowed = original.Allowed, // allowed can only come from master / fallback
    //            Anonymous = Merge(priority.Anonymous, original.Anonymous),
    //            Admin = Merge(priority.Admin, original.Admin),
    //        };

    private bool Merge(bool? priority, bool? fallback) => priority == true || priority == null && fallback == true;

    internal MagicDebugState Parsed(bool isAdmin)
    {
        if (!(Allowed ?? AllowedDefault))
            return new();

        return new()
        {
            Show = isAdmin
                ? Admin ?? AdminDefault
                : Anonymous ?? AnonymousDefault
        };
    }

    internal static Defaults<MagicDebugSettings> Defaults = new(new()
    {
        Allowed = false,
        Anonymous = false,
        Admin = false,
    });
}