namespace ToSic.Cre8magic.Settings.Providers.Internal;

public class MagicProviderSection<T>(IMagicSettingsProvider parent) : IMagicProviderSectionWithMoreWip<T, IMagicSettingsProvider> where T : class
{
    public bool HasValues { get; private set; }
    internal T? Value { get; set; }

    internal IDictionary<string, T>? Values { get; set; }

    internal Func<IMagicSettingsContext, T?>? Getter;


    public T? Find(IMagicSettingsContext context) => ProviderFindWip.StaticFind(context, Getter, Value, Values);


    public IMagicSettingsProvider Provide(T value)
    {
        Value = value;
        HasValues = true;
        return parent;
    }

    public IMagicSettingsProvider Provide(string key, T value)
    {
        Values ??= new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
        Values[key] = value;
        HasValues = true;
        return parent;
    }

    public IMagicSettingsProvider Provide(IDictionary<string, T> dictionary)
    {
        Values = new Dictionary<string, T>(dictionary, StringComparer.InvariantCultureIgnoreCase);
        HasValues = true;
        return parent;
    }

    public IMagicSettingsProvider Provide(Func<IMagicSettingsContext, T> getter)
    {
        Getter = getter;
        HasValues = true;
        return parent;
    }
}