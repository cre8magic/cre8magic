namespace ToSic.Cre8magic.Internal;

public class Debouncer<T> where T : class
{
    private T _value;
    private bool generated;

    public Debouncer(T value)
    {
        Generate = () => value;
        KeepFunc = () => true;
    }

    public Debouncer(Func<T> generate, Func<bool> keepFunc)
    {
        Generate = generate;
        KeepFunc = keepFunc;
    }

    public Debouncer(Func<T> generate, Func<object?> getThingToCompare)
    {
        Generate = generate;
        KeepFunc = () =>
        {
            var compare = getThingToCompare();
            if (compare == null && _lastCompared == null)
                return true;
            if (_lastCompared == null)
            {
                _lastCompared = compare;
                return false;
            }
            if (_lastCompared.Equals(compare))
                return true;

            _lastCompared = compare;
            return false;
        };
    }

    private object? _lastCompared;

    private Func<T> Generate { get; }
    private Func<bool> KeepFunc { get; }

    public T Get()
    {
        if (generated && KeepFunc())
            return _value;
        generated = true;
        return _value = Generate();
    }
}