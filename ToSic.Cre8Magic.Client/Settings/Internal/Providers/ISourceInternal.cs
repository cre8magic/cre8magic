namespace ToSic.Cre8magic.Settings.Internal.Providers;

internal interface ISourceInternal
{
    bool HasValues { get; }

    void Reset();
}