
namespace ToSic.Cre8magic.Utils;

/// <summary>
/// Get-or-Keep helper.
/// Will expect a second return value when generating the value, which it will ask for comparison
/// every time it's accessed again.
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCompare"></typeparam>
[PrivateApi]
public class GetKeep<TResult, TCompare> where TResult : class
{
    private TResult? _value;

    private TCompare? _compare;

    public TResult Get(Func<(TResult Result, TCompare Compare)> getter, Func<TResult?, TCompare?, bool> keep)
    {
        if (IsValueCreated && keep(_value, _compare))
            return _value!;

        var result = getter();
        _value = result.Result;
        _compare = result.Compare;
        IsValueCreated = true;
        return _value;
    }

    // Create the same as above, but Async

    public async Task<TResult> GetAsync(Func<Task<(TResult Result, TCompare Compare)>> getter, Func<TResult?, TCompare?, bool> keep)
    {
        if (IsValueCreated && keep(_value, _compare))
            return _value!;
        var result = await getter();
        _value = result.Result;
        _compare = result.Compare;
        IsValueCreated = true;
        return _value;
    }


    /// <summary>
    /// Determines if value has been created.
    /// The name `IsValueCreated` is the same as in a Lazy() object
    /// </summary>
    public bool IsValueCreated { get; private set; }

}