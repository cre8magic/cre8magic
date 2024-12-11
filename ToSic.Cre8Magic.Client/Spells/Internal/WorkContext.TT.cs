namespace ToSic.Cre8magic.Spells.Internal;

/// <summary>
/// The execution context - WIP.
/// Goal is that it contains logging etc. which is shared across all parts which are getting something done.
/// </summary>
internal record WorkContext<TSettings, TTailor> : WorkContext
{
    public required TSettings Settings { get; init; }

    // TODO: ATM still nullable, should be changed
    public required TTailor? Tailor { get; init; }

}