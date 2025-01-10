namespace ToSic.Cre8magic.Settings.Providers.Internal;

internal interface ISourceInternal
{
    bool HasValues { get; }

    void Reset();
}