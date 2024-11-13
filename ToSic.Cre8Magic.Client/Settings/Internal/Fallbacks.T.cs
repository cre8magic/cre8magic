namespace ToSic.Cre8magic.Settings.Internal;

internal class Defaults<T> where T: class, new()
{
    public Defaults() {}

    public Defaults(T both)
    {
        Fallback = both;
        Foundation = both;
    }

    public Defaults(T fallback, T? foundation)
    {
        Fallback = fallback;
        Foundation = foundation;
    }

    public T Fallback { get; set; } = new();
    public T? Foundation { get; set; } = null;
}