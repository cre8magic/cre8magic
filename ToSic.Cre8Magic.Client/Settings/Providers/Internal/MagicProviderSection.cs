using ToSic.Cre8magic.Settings.Internal.Experimental;

namespace ToSic.Cre8magic.Settings.Providers.Internal;

public class MagicProviderSection<T>(IMagicSettingsProvider parent)
    : IMagicProviderSectionWithMoreWip<T, IMagicSettingsProvider>, ISourceInternal
    where T : class
{
    public bool HasValues { get; private set; }

    internal IDictionary<string, T>? Values { get; set; }

    internal Func<IMagicSettingsContext, T?>? Getter;

    public void Reset()
    {
        Values = null;
        Getter = null;
        HasValues = false;
    }

    // TODO: use constant for "Default"
    public IMagicSettingsProvider ProvideDefault(T value) =>
        Provide("default", value);

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