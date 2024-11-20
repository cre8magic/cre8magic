namespace ToSic.Cre8magic.Settings.Providers.Internal;

public static class ProviderFindWip
{
    public static T? Find<T>(this IMagicProviderSection<T> source, IMagicSettingsContext context) where T : class
    {
        if (source is not MagicProviderSection<T> typedSource)
            throw new ArgumentException("source must be of type MagicProviderSection<T>", nameof(source));

        return StaticFind(context, typedSource.Getter, typedSource.Value, typedSource.Values);
    }

    public static T? StaticFind<T>(IMagicSettingsContext context, Func<IMagicSettingsContext, T?>? getter, T? value, IDictionary<string, T>? values) where T : class
    {
        // First try if we have a custom getter - as it has the highest priority
        if (getter != null)
            return getter(context) ?? (context.FallbackToDefault
                    ? value
                    : null
                );

        // If we have a Dictionary, try that
        if (values != null)
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
                if (!string.IsNullOrEmpty(key) && values.TryGetValue(key, out var namedValue))
                    return namedValue;
        }

        return context.FallbackToDefault ? value : null;
    }
}