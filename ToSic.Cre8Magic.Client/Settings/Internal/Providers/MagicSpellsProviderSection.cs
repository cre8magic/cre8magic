namespace ToSic.Cre8magic.Settings.Internal.Providers;

internal class MagicSpellsProviderSection<T>(IMagicSpellsProvider parent)
    : IMagicSpellsProviderSection<T>, ISourceInternal
    where T : class
{
    public bool HasValues { get; private set; }

    internal IDictionary<string, T>? Values { get; set; }

    public void Reset()
    {
        Values = null;
        HasValues = false;
    }

    // TODO: use constant for "Default"
    public IMagicSpellsProvider SetDefault(T value) =>
        Provide("default", value);

    public IMagicSpellsProvider Provide(string key, T value)
    {
        Values ??= new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
        Values[key] = value;
        HasValues = true;
        return parent;
    }

    public IMagicSpellsProvider Provide(IDictionary<string, T> dictionary)
    {
        Values = new Dictionary<string, T>(dictionary, StringComparer.InvariantCultureIgnoreCase);
        HasValues = true;
        return parent;
    }

}