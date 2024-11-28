namespace ToSic.Cre8magic.Utils;

internal class GetOnce<TResult> 
{
    /// <summary>
    /// Construct an empty GetOnce object for use later on.
    ///
    /// In case you're wondering why we can't pass the generator in on the constructor:
    /// Reason is that in most cases we need real objects in the generator function,
    /// which doesn't work in a `static` context.
    /// This means that if the = new GetOnce() is run on the private property
    /// (which is the most common case) most generators can't be added. 
    /// </summary>
    public GetOnce() { }

    /// <summary>
    /// Get the value. If not yet retrieved, use the generator function (but only once). 
    /// </summary>
    /// <param name="generator">Function which will generate the value on first use.</param>
    /// <returns></returns>
    public TResult Get(Func<TResult> generator)
    {
        if (IsValueCreated) return _value!;
        // Important: don't use try/catch, because the parent should be able to decide if try/catch is appropriate
        _value = generator();
        // Important: This must happen explicitly after the generator() - otherwise there is a risk of cyclic code which already assume
        // the value was created, while still inside the creation of the value.
        // So we would rather have a stack overflow and find the problem code, than to let the code assume the value was already made and null
        IsValueCreated = true;
        return _value;
    }

    public TResult Get(Func<TResult> getter, Func<TResult, bool> keep)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        if (IsValueCreated && keep(_value))
#pragma warning restore CS8604 // Possible null reference argument.
            return _value;

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