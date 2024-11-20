namespace ToSic.Cre8magic.Settings.Providers.Internal;

public static class ProviderFindWip
{
    /// <summary>
    /// Retrieve specified settings according to specs in the context.
    ///
    /// Will search all providers according to the following priority:
    ///
    /// 1. First it will check if there is a function provider
    /// 2. Then it will check if a provider is available in the dictionary using the name and optionally the prefix in the context
    /// 3. Then it will check for a `default` provider in the dictionary, or a `prefix-default`
    /// 4. Finally, it will check the simple Value provider
    /// 5. Last but not least: null
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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