
namespace ToSic.Cre8magic.Utils;

/// <summary>
/// Get-or-Keep helper.
/// Will expect a second return value when generating the value, which it will ask for comparison
/// every time it's accessed again.
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCompare"></typeparam>
public class GetKeep<TResult, TCompare> where TResult : class
{
    public TResult Get(Func<(TResult Result, TCompare Compare)> getter, Func<TResult, TCompare, bool> keep)
    {
        if (IsValueCreated && keep(_value, compare)) return _value;

        var result = getter();
        _value = result.Result;
        compare = result.Compare;
        IsValueCreated = true;
        return _value;
    }
    private TResult? _value;

    private TCompare compare;

    /// <summary>
    /// Determines if value has been created.
    /// The name `IsValueCreated` is the same as in a Lazy() object
    /// </summary>
    public bool IsValueCreated { get; private set; }

}