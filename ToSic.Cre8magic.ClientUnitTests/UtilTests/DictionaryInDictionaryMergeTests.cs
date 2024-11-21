using ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.ClientUnitTests.UtilTests;

public class DictionaryInDictionaryMergeTests
{
    [Fact]
    public void DictionaryString()
    {
        // create a dictionary inside a dictionary
        var priority = new Dictionary<string, Dictionary<string, string>>
        {
            { "key1", new() { { "key1", "value1" }, { "key2", "value2" } } },
            //{ "key2", new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } } }
        };

        var fallback = new Dictionary<string, Dictionary<string, string>>
        {
            //{ "key1", new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } } },
            { "key2", new() { { "key1", "value1" }, { "key2", "value2" } } }
        };

        // merge the dictionaries
        var result = MergeHelper.TryToMergeOrKeepPriority(priority, fallback);

        // check if the result is correct
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result["key1"].Count);
        Assert.Equal("value1", result["key1"]["key1"]);
        Assert.Equal("value2", result["key1"]["key2"]);

    }

    [Fact]
    public void DictionaryWithClonableObject()
    {
        // create a dictionary inside a dictionary
        var priority = new Dictionary<string, Dictionary<string, TestDataAbleToMerge>>
        {
            { "key1", new() { { "key1", new() { Name = "iJungleboy" }}, { "key2", new() } } },
        };

        var fallback = new Dictionary<string, Dictionary<string, TestDataAbleToMerge>>
        {
            { "key1", new() { { "key1", new() { Id = 42 } }, { "key2", new() } } },
            { "key2", new() { { "key1", new() { Name = "Key2-Key1" } }, { "key2", new() } } }
        };

        // merge the dictionaries
        var result = MergeHelper.TryToMergeOrKeepPriority(priority, fallback);

        // check if the result is correct
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result["key1"].Count);
        Assert.Equal("iJungleboy", result["key1"]["key1"].Name);
        Assert.Equal(42, result["key1"]["key1"].Id);

    }
}