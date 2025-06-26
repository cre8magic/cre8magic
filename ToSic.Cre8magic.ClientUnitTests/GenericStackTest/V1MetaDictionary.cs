using System.Runtime.CompilerServices;

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

public class V1MetaDictionary
{
    public Dictionary<Type, object> Stack { get; set; } = new();

    public Dictionary<string, T> GetTypeDic<T>()
    {
        var type = typeof(T);
        if (Stack.TryGetValue(type, out var dic) && dic is Dictionary<string, T> typedDic)
            return typedDic;

        // Important: we don't want to use Invariant, as it lowers the performance by ca. factor 3!
        var newDic = new Dictionary<string, T>();
        Stack[type] = newDic;
        return newDic;
    }

    public bool Contains<T>(string key) => GetTypeDic<T>().ContainsKey(key);

    public void Add<T>(string key, T item) => GetTypeDic<T>()[key] = item;

    public void AddThis<T>(T item, [CallerMemberName] string key = default!) => GetTypeDic<T>()[key] = item;

    public T Get<T>(string key) => GetTypeDic<T>()[key];

    public T Get<T>(T defaultValue, string key) => GetTypeDic<T>().GetValueOrDefault(key, defaultValue);

    public T GetThis<T>(T defaultValue = default!, [CallerMemberName] string key = default!) => GetTypeDic<T>().GetValueOrDefault(key, defaultValue);
}