namespace ToSic.Cre8magic.Settings.Internal;

public class MagicSettingsProvider<T>(IMagicSettingsProviders parent) : IMagicSettingsProvider<T> where T : class
{
    private T? Value { get; set; }

    private IDictionary<string, T>? Values { get; set; }

    private Func<IMagicSettingsContext, T?>? _getter;


    public T? Find(IMagicSettingsContext context)
    {
        // First try if we have a custom getter - as it has the highest priority
        if (_getter != null)
            return _getter(context) ?? (context.FallbackToDefault
                    ? Value
                    : null
                );

        // If we have a Dictionary, try that
        if (Values != null)
        {
            var hasPrefix = !string.IsNullOrEmpty(context.Prefix);
            var hasName = !string.IsNullOrEmpty(context.Name);
            List<string?> keys =
            [
                hasPrefix ? context.Prefix + "-" + context.Name : null,
                hasName ? context.Name : null,
                hasPrefix ? context.Prefix + "-" + "default" : null,
                context.FallbackToDefault ? "default" : null,
            ];
            foreach (var key in keys)
                if (!string.IsNullOrEmpty(key) && Values.TryGetValue(key, out var namedValue))
                    return namedValue;
        }

        return context.FallbackToDefault ? Value : null;
    }

    public IMagicSettingsProviders Provide(T value)
    {
        Value = value;
        return parent;
    }

    public IMagicSettingsProviders Provide(string key, T value)
    {
        Values ??= new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
        Values[key] = value;
        return parent;
    }

    public IMagicSettingsProviders Provide(IDictionary<string, T> dictionary)
    {
        Values = new Dictionary<string, T>(dictionary, StringComparer.InvariantCultureIgnoreCase);
        return parent;
    }

    public IMagicSettingsProviders Provide(Func<IMagicSettingsContext, T> getter)
    {
        _getter = getter;
        return parent;
    }
}