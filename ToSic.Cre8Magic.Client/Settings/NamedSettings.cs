using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;
using static System.StringComparer;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Case-insensitive dictionary managing a list of named settings
/// </summary>
/// <typeparam name="T"></typeparam>
public class NamedSettings<T>: Dictionary<string, T>, ICanClone<NamedSettings<T>> where T : class
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

        // Both existed, so the original one was created with the fallback
        // now merge in the priority
        MergeHelper.MergeDictionaries(this, priority);
    }

    public NamedSettings<T> CloneUnder(NamedSettings<T>? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? new(this) : this) : new(priority, this);


}