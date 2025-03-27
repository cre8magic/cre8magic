using System.Runtime.CompilerServices;

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

public class V3ObjectDictionaryNonGeneric
{
    public Dictionary<string, object> Stack { get; set; } = new();

    public bool Contains(string key) => Stack.ContainsKey(key);

    public void Add(string key, object item) => Stack[key] = item;

    public void AddThis(object item, [CallerMemberName] string key = default!) => Stack[key] = item;

    public T Get<T>(string key) => (T)Stack[key];

    public T Get<T>(T defaultValue, string key) => (T)Stack.GetValueOrDefault(key, defaultValue!);

    public T GetThis<T>(T defaultValue = default!, [CallerMemberName] string key = default!) => (T)Stack.GetValueOrDefault(key, defaultValue!);

    //public Dictionary<string, object> CloneUnder(Dictionary<string, object>? priority)
    //{
    //    var newDic = new Dictionary<string, object>(Stack);
    //    if (priority == null)
    //        return newDic;

    //    foreach (var (key, value) in priority)
    //        newDic[key] = value;
    //    return newDic;
    //}

    /// <summary>
    /// This alt implementation tries to be smarter, but it seems that it's almost the same, and sometimes even slower.
    /// </summary>
    /// <param name="priority"></param>
    /// <returns></returns>
    public Dictionary<string, object> CloneUnderAlt(Dictionary<string, object>? priority)
    {
        if (priority == null)
            return new(Stack);

        var newDic = new Dictionary<string, object>(priority);

        foreach (var (key, value) in priority)
            newDic.TryAdd(key, value);
        return newDic;
    }
}