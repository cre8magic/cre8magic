using ToSic.Cre8magic.Settings.Internal.ProvidersWip;

namespace ToSic.Cre8magic.Settings.Internal;

public class MagicSettingsProvider<T>(IMagicSettingsProviders parent) : IMagicSettingsProviderWithMoreWip<T, IMagicSettingsProviders> where T : class
{
    internal bool HasValues { get; private set; }
    private T? Value { get; set; }

    internal IDictionary<string, T>? Values { get; set; }

    private Func<IMagicSettingsContext, T?>? _getter;


    public T? Find(IMagicSettingsContext context) => ProviderFindWip.StaticFind(context, _getter, Value, Values);


    public IMagicSettingsProviders Provide(T value)
    {
        Value = value;
        HasValues = true;
        return parent;
    }

    public IMagicSettingsProviders Provide(string key, T value)
    {
        Values ??= new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
        Values[key] = value;
        HasValues = true;
        return parent;
    }

    public IMagicSettingsProviders Provide(IDictionary<string, T> dictionary)
    {
        Values = new Dictionary<string, T>(dictionary, StringComparer.InvariantCultureIgnoreCase);
        HasValues = true;
        return parent;
    }

    public IMagicSettingsProviders Provide(Func<IMagicSettingsContext, T> getter)
    {
        _getter = getter;
        HasValues = true;
        return parent;
    }
}