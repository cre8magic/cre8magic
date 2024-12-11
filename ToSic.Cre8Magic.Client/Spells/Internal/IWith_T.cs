namespace ToSic.Cre8magic.Spells.Internal;

public interface IWith<T>
{
    public T? WithData { get; init; }
}