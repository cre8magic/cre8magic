namespace ToSic.Cre8magic.Spells.Internal;

[PrivateApi]
public interface ICanClone<T>
{
    /// <summary>
    /// Clone the current object under another one which is more important than this one.
    /// </summary>
    /// <param name="priority"></param>
    /// <param name="forceCopy"></param>
    /// <returns></returns>
    public T CloneUnder(T? priority, bool forceCopy = false);
}