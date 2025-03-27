using System.Runtime.CompilerServices;
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8604 // Possible null reference argument.

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

public class V2ObjectDictionary
{
    public Dictionary<string, object> Stack { get; set; } = new();

    public bool Contains<T>(string key) => Stack.ContainsKey(key);

    public void Add<T>(string key, T item) => Stack[key] = item;

    public void AddThis<T>(T item, [CallerMemberName] string key = default) => Stack[key] = item;

    public T Get<T>(string key) => (T)Stack[key];

    public T Get<T>(T defaultValue, string key) => (T)Stack.GetValueOrDefault(key, defaultValue);

    public T GetThis<T>(T defaultValue = default, [CallerMemberName] string key = default) => (T)Stack.GetValueOrDefault(key, defaultValue);
}