using ToSic.Cre8magic.Settings.Internal;
using static System.StringComparer;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Case-insensitive dictionary managing a list of named settings
/// </summary>
/// <typeparam name="T"></typeparam>
// TODO: CONTINUE HERE - ICANCLONE REQUIREMENT
public class NamedSettings<T>: Dictionary<string, T> where T : class // , ICanClone<T>
{
    public NamedSettings() : base(InvariantCultureIgnoreCase) { }

    public NamedSettings(IDictionary<string, T> dic): base(dic, InvariantCultureIgnoreCase) { }

    public NamedSettings(IEnumerable<KeyValuePair<string, T>> dic): base(dic, InvariantCultureIgnoreCase) { }

    public T? GetInvariant(string key) => TryGetValue(key, out var value) ? value : default;

}