
namespace ToSic.Cre8magic.Utils;

internal class GetKeep<TResult> where TResult : class
{
    public TResult Get(Func<TResult> getter, Func<TResult?, bool> keep)
    {
        if (IsValueCreated && keep(_value))
            return _value!;

        _value = getter();
        IsValueCreated = true;
        return _value;
    }
    private TResult? _value;

    /// <summary>
    /// Determines if value has been created.
    /// The name `IsValueCreated` is the same as in a Lazy() object
    /// </summary>
    public bool IsValueCreated { get; private set; }

}