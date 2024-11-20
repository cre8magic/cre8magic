using ToSic.Cre8magic.Settings.Internal;
using static System.StringComparer;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Case-insensitive dictionary managing a list of named settings
/// </summary>
/// <typeparam name="T"></typeparam>
public class NamedSettings<T>: Dictionary<string, T>, ICanClone<NamedSettings<T>> where T : class, ICanClone<T>
{
    public NamedSettings() : base(InvariantCultureIgnoreCase) { }

    public NamedSettings(IDictionary<string, T> dic) : base(dic, InvariantCultureIgnoreCase) { }

    /// <summary>
    /// Copy / clone constructor
    /// </summary>
    /// <param name="priority"></param>
    /// <param name="fallback"></param>
    public NamedSettings(NamedSettings<T>? priority, NamedSettings<T>? fallback = default) : base(fallback ?? priority ?? new(), InvariantCultureIgnoreCase)
    {
        // If either of the sources are null, then it was already merged in the base(...) call, so exit
        if (priority == null || fallback == null)
            return;

        // Merge the priority over the fallback settings
        foreach (var (key, value) in priority)
        {
            // If it doesn't exist yet, simply add
            if (!ContainsKey(key))
            {
                Add(key, value);
                continue;
            }

            // If it does exist, and it's a cloneable type, then clone and merge
            if (value is ICanClone<T> cloneable)
            {
                // since both sources exist, the dictionary already contains the fallback
                var existingFallback = this[key];
                this[key] = existingFallback.CloneUnder(value, true);
                continue;
            }

            // If it's not cloneable, then just replace
            this[key] = value;
        }
    }

    public NamedSettings<T> CloneUnder(NamedSettings<T>? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? new(this) : this) : new(priority, this);



}