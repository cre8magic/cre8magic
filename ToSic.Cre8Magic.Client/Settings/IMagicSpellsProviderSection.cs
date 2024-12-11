namespace ToSic.Cre8magic.Settings;

/// <summary>
/// A provider for one type of settings.
/// </summary>
/// <typeparam name="TSettings"></typeparam>
public interface IMagicSpellsProviderSection<TSettings> where TSettings : class
{
    /// <summary>
    /// Configure to provide a value - either as the only result for all requests, or as a default.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IMagicSpellsProvider SetDefault(TSettings value);

    /// <summary>
    /// Configure to provide a named value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IMagicSpellsProvider Provide(string key, TSettings value);

    /// <summary>
    /// Configure to provide a dictionary of named values.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    IMagicSpellsProvider Provide(IDictionary<string, TSettings> dictionary);

    internal bool HasValues { get; }
}