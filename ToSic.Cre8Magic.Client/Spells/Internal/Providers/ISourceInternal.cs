namespace ToSic.Cre8magic.Spells.Internal.Providers;

internal interface ISourceInternal
{
    bool HasValues { get; }

    void Reset();
}