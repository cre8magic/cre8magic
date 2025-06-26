using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

public class NamedSettingsCreateClone
{
    [Theory]
    [InlineData(true)]
    public void Cloning(bool forceCopy)
    {
        var original = DataSimple;
        var result = MergeHelper.CloneMergeDictionaries(original, null);
        Assert.Equal(forceCopy, result != original);
        AssertSameAs(original, result);
    }

    private static void AssertSameAs(Dictionary<string, TestDataNoMerge> expected, Dictionary<string, TestDataNoMerge> clone)
    {
        Assert.Equal(expected.Count, clone.Count);
        foreach (var key in expected.Keys)
        {
            Assert.True(clone.ContainsKey(key));
            Assert.Equal(expected[key], clone[key]);
            Assert.Equal(expected[key].Name, clone[key].Name);
            Assert.Equal(expected[key].Id, clone[key].Id);
            Assert.Equal(expected[key].Description, clone[key].Description);
        }
    }


    [Fact]
    public void MixingHalves() => MixExpectsDataSimple(DataSimpleHalf2, DataSimpleHalf1);

    [Fact]
    public void MixingPartsWhichOverlapWithIdenticalData() => MixExpectsDataSimple(DataSimpleHalf2, DataSimpleHalf1);

    private static void MixExpectsDataSimple(Dictionary<string, TestDataNoMerge> priority, Dictionary<string, TestDataNoMerge> fallback)
    {
        var result = MergeHelper.CloneMergeDictionaries(priority, fallback);
        AssertSameAs(DataSimple, result);
    }

    private static Dictionary<string, TestDataNoMerge> DataSimple =>
        new()
        {
            { "a", new() },
            { "name", new() { Name = "hello" } },
            { "ID", new() { Id = 123 } },
            { "description", new() { Description = "world" } },
        };


    private static Dictionary<string, TestDataNoMerge> DataSimpleHalf1 =>
        new()
        {
            { "a", new() },
            { "name", new() { Name = "hello" } },
            //{ "ID", new DataForTest { Id = 123 } },
            //{ "description", new DataForTest { Description = "world" } },
        };

    private static Dictionary<string, TestDataNoMerge> DataSimpleHalf2 =>
        new()
        {
            //{ "a", new DataForTest() },
            //{ "name", new DataForTest { Name = "hello" } },
            { "ID", new() { Id = 123 } },
            { "description", new() { Description = "world" } },
        };
    private static Dictionary<string, TestDataNoMerge> DataSimpleHalf2WithName =>
        new()
        {
            //{ "a", new DataForTest() },
            { "name", new TestDataNoMerge { Name = "hello" } },
            { "ID", new() { Id = 123 } },
            { "description", new() { Description = "world" } },
        };

}