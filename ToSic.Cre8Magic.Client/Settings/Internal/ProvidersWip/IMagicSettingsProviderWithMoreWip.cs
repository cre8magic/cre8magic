namespace ToSic.Cre8magic.Settings;

/// <summary>
/// A provider for one type of settings.
/// </summary>
/// <typeparam name="TSettings"></typeparam>
/// <typeparam name="TProvider"></typeparam>
public interface IMagicSettingsProviderWithMoreWip<TSettings, out TProvider> : IMagicSettingsProvider<TSettings, TProvider> where TSettings : class
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
    TSettings? Find(IMagicSettingsContext context);

    ///// <summary>
    ///// Configure to provide a value - either as the only result for all requests, or as a default.
    ///// </summary>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //TProvider Provide(TSettings value);

    ///// <summary>
    ///// Configure to provide a named value.
    ///// </summary>
    ///// <param name="key"></param>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //TProvider Provide(string key, TSettings value);

    ///// <summary>
    ///// Configure to provide a dictionary of named values.
    ///// </summary>
    ///// <param name="dictionary"></param>
    ///// <returns></returns>
    //TProvider Provide(IDictionary<string, TSettings> dictionary);

    /// <summary>
    /// Configure to provide a function to retrieve the value, which can contain more sophisticated logic.
    /// </summary>
    /// <param name="getter"></param>
    /// <returns></returns>
    TProvider Provide(Func<IMagicSettingsContext, TSettings> getter);
}