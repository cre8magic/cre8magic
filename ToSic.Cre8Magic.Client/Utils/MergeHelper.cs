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
            MergeDictionariesUnknownType(fallbackDic, priorityDic);
            return fallback;
        }

        if (fallback is IDictionary<string, TPart> dict && priority is IDictionary<string, TPart> priorityDict)
        {
            MergeDictionaries(dict, priorityDict);
            return fallback;
        }

        return priority;
    }

    public static void MergeDictionaries<TVal>(IDictionary<string, TVal>? target, IDictionary<string, TVal>? priority)
    {
        if (priority == null || target == null)
            return;

        // Merge the priority over the fallback settings
        foreach (var (key, value) in priority)
        {
            // If it doesn't exist yet, simply add
            if (target.TryAdd(key, value))
                continue;

            // If it does exist, and it's a cloneable type, then clone and merge
            target[key] = TryToMergeOrKeepPriority(value, target[key]);
        }
    }

    public static void MergeDictionariesUnknownType(IDictionary target, IDictionary source)
    {
        try
        {
            var typeTarget = target.GetType();
            var typeSource = source.GetType();
            if (typeTarget != typeSource || !typeSource.IsGenericType)
                return;

            var typeVal = typeSource.GetGenericArguments()[1];
            var method = typeof(MergeHelper).GetMethod(nameof(MergeDictionaries))?.MakeGenericMethod(typeVal);
            method?.Invoke(null, [target, source]);
        }
        catch
        {
            // ignored
        }
    }
}