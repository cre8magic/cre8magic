namespace ToSic.Cre8magic.Settings.Internal;

public interface ICanClone<T>
{
    public T CloneMerge(T? priority, bool forceCopy = false);
}