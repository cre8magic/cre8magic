namespace ToSic.Cre8magic.Settings.Internal;

internal interface ICanClone<T>
{
    public T CloneMerge(T? priority, bool forceCopy = false);
}