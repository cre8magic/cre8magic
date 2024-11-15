namespace ToSic.Cre8magic.Core;

/// <summary>
/// WIP
/// </summary>
public interface IProvide<out T> where T : class
{
    ///// <summary>
    ///// WIP
    ///// </summary>
    //public T? Get();

    /// <summary>
    /// WIP
    /// </summary>
    public T? Get(string name);
}