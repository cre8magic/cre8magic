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

        Dictionary<string, string> target = new()
        {
            { "Key2", "Value2" }
        };

        MergeHelper.MergeDictionaries(target, priority);

        Assert.Equal(2, target.Count);
        Assert.Equal("Value1", target["Key1"]);
        Assert.Equal("Value2", target["Key2"]);
    }

    [Fact]
    public void MergeWithSameKeys()
    {
        Dictionary<string, string> priority = new()
        {
            { "Key1", "Value1" }
        };

        Dictionary<string, string> target = new()
        {
            { "Key1", "Value2" },
            { "Key2", "Value2" }
        };

        MergeHelper.MergeDictionaries(target, priority);

        Assert.Equal(2, target.Count);
        Assert.Equal("Value1", target["Key1"]);
        Assert.Equal("Value2", target["Key2"]);
    }

    [Fact]
    public void MergeClonable()
    {
        Dictionary<string, TestDataAbleToMerge> priority = new()
        {
            { "Key1", new() { Name = "iJungleboy"} }
        };

        Dictionary<string, TestDataAbleToMerge> target = new()
        {
            { "Key1", new() { Id = 42 } },
            { "Key2", new() }
        };

        MergeHelper.MergeDictionaries(target, priority);

        Assert.Equal(2, target.Count);
        Assert.Equal("iJungleboy", target["Key1"].Name);
        Assert.Equal(42, target["Key1"].Id);
    }

}