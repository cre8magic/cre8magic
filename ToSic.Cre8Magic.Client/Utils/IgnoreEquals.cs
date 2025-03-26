namespace ToSic.Cre8magic.Utils;

/// <summary>
/// Special helper to store a value on a record but not include it in the Equality contract.
///
/// Typical use case is calculated properties which contain the record itself, which would result in infinite loops.
///
/// https://github.com/dotnet/csharplang/discussions/4257#discussioncomment-11263857
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <param name="value"></param>
public readonly struct IgnoreEquals<TValue>(TValue value)
{
    public TValue Value { get; init; } = value;
    public override bool Equals(object? obj) => true;
    public override int GetHashCode() => 0;
    public override string? ToString() => Value?.ToString();
}