using ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.ClientUnitTests.UtilTests;

public class DictionaryMergeTests
{
    [Fact]
    public void MergeWithDifferentKeys()
    {
        Dictionary<string, string> priority = new()
        {
            { "Key1", "Value1" }
        };

        Dictionary<string, string> fallback = new()
        {
            { "Key2", "Value2" }
        };

        var result = MergeHelper.CloneMergeDictionaries(priority, fallback);

        Assert.Equal(2, result.Count);
        Assert.Equal("Value1", result["Key1"]);
        Assert.Equal("Value2", result["Key2"]);
    }

    [Fact]
    public void MergeWithSameKeys()
    {
        Dictionary<string, string> priority = new()
        {
            { "Key1", "Value1" }
        };

        Dictionary<string, string> fallback = new()
        {
            { "Key1", "Value2" },
            { "Key2", "Value2" }
        };

        var result = MergeHelper.CloneMergeDictionaries(priority, fallback);

        Assert.Equal(2, result.Count);
        Assert.Equal("Value1", result["Key1"]);
        Assert.Equal("Value2", result["Key2"]);
    }

    [Fact]
    public void MergeClonable()
    {
        Dictionary<string, TestDataAbleToMerge> priority = new()
        {
            { "Key1", new() { Name = "iJungleboy"} }
        };

        Dictionary<string, TestDataAbleToMerge> fallback = new()
        {
            { "Key1", new() { Id = 42 } },
            { "Key2", new() }
        };

        var result = MergeHelper.CloneMergeDictionaries(priority, fallback);

        Assert.Equal(2, result.Count);
        Assert.Equal("iJungleboy", result["Key1"].Name);
        Assert.Equal(42, result["Key1"].Id);
    }

}