namespace ToSic.Cre8magic.Settings.Providers.Internal;

internal class MagicSettingsProviderSection<T>(IMagicSettingsProvider parent)
    : IMagicSettingsProviderSection<T>, ISourceInternal
    where T : class
{
    public bool HasValues { get; private set; }

    internal IDictionary<string, T>? Values { get; set; }

    public void Reset()
    {
        Values = null;
        HasValues = false;
    }

    //// TODO: use constant for "Default"
    //public IMagicSettingsProvider SetDefault(T value) =>
    //    Provide("default", value);

    //public IMagicSettingsProvider Provide(string key, T value)
    //{
    //    Values ??= new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
    //    Values[key] = value;
    //    HasValues = true;
    //    return parent;
    //}

    //public IMagicSettingsProvider Provide(IDictionary<string, T> dictionary)
    //{
    //    Values = new Dictionary<string, T>(dictionary, StringComparer.InvariantCultureIgnoreCase);
    //    HasValues = true;
    //    return parent;
    //}

}