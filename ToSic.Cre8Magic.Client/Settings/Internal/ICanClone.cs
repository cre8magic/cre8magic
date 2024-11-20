namespace ToSic.Cre8magic.Settings.Internal;

public interface ICanClone<T>
{
    public T CloneUnder(T? priority, bool forceCopy = false);
}