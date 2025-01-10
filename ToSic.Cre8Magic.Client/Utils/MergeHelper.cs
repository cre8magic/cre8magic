using System.Collections;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Utils;

internal class MergeHelper
{
    
    public static TPart TryToMergeOrKeepPriority<TPart>(TPart? priority, TPart fallback)
    {
        if (priority == null)
            return fallback;

        if (fallback is ICanClone<TPart> cloneable)
            return cloneable.CloneUnder(priority);

        if (fallback is IDictionary fallbackDic && priority is IDictionary priorityDic)
        {
            var mergedDic = MergeDictionariesUnknownType(fallbackDic, priorityDic);
            return mergedDic is TPart partDic ? partDic : priority;
        }

        return priority;
    }

    /// <summary>
    /// Clone merge dictionaries - will always return a new invariant dictionary, never null.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    /// <param name="priority"></param>
    /// <param name="fallback"></param>
    /// <returns>Will always return a dictionary invariant, never null.</returns>
    public static Dictionary<string, TVal> CloneMergeDictionaries<TVal>(Dictionary<string, TVal>? priority, Dictionary<string, TVal>? fallback)
    {
        if (priority == null && fallback == null)
            return new(StringComparer.InvariantCultureIgnoreCase);

        if (priority == null || fallback == null)
            return new(fallback ?? priority!, StringComparer.InvariantCultureIgnoreCase);

        var target = new Dictionary<string, TVal>(fallback, StringComparer.InvariantCultureIgnoreCase);

        // Merge the priority over the fallback settings
        foreach (var (key, value) in priority)
        {
            // If it doesn't exist yet, simply add
            if (target.TryAdd(key, value))
                continue;

            // If it does exist, and it's a cloneable type, then clone and merge
            target[key] = TryToMergeOrKeepPriority(value, target[key]);
        }
        return target;
    }


    private static IDictionary MergeDictionariesUnknownType(IDictionary target, IDictionary priority)
    {
        try
        {
            var typeTarget = target.GetType();
            var typeSource = priority.GetType();
            if (typeTarget != typeSource || !typeSource.IsGenericType)
                return priority;

            var typeVal = typeSource.GetGenericArguments()[1];
            var method = typeof(MergeHelper).GetMethod(nameof(CloneMergeDictionaries))?.MakeGenericMethod(typeVal);
            var result = method?.Invoke(null, [target, priority]);
            return result as IDictionary ?? priority;
        }
        catch
        {
            // ignored
            return priority;
        }
    }
}