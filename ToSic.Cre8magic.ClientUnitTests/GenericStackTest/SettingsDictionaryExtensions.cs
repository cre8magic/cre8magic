using System.Runtime.CompilerServices;

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

/// <summary>
/// 2025-03-26 2dm
/// Tested this idea extensively, but it won't work.
///
/// This model assumed that every property would have a get which returned the default value if the key wasn't found.
/// But that would mean that when cloning a record, the new clone would get all the properties of the original,
/// and then contain real values, even though they were never explicitly set.
/// So this idea would fail.
///
/// I'm leaving the code in so that the next person trying this will realize that it's not a valid idea. 
/// </summary>
public static class SettingsDictionaryExtensions
{
    public static T GetThis<T>(this Dictionary<string, object> dic, T defaultValue = default, [CallerMemberName] string key = default) =>
        dic.TryGetValue(key, out var tResult)
            ? tResult is T typedResult ? typedResult : defaultValue
            : defaultValue;

    public static void AddThis(this Dictionary<string, object> dic, object? item, [CallerMemberName] string key = default)
    {
        if (item == null)
            dic.Remove(key);
        else
            dic[key] = item;
    }


    public static Dictionary<string, object> CloneUnder(this Dictionary<string, object>? original, Dictionary<string, object>? priority)
    {
        if (priority == null)
            return original == null ? new() : new(original);

        if (original == null)
            return new(priority);

        var newDic = new Dictionary<string, object>(original);

        foreach (var (key, value) in priority)
            newDic[key] = value;
        return newDic;
    }
}