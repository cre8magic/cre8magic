namespace ToSic.Cre8magic.Settings.Internal;

public interface ICanClone<T>
{
    public T CloneWith(T? priority, bool forceCopy = false);
}