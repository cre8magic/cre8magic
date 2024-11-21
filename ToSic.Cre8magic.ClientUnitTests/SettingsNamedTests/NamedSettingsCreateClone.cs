using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

public class NamedSettingsCreateClone
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Cloning(bool forceCopy)
    {
        var original = DataSimple;
        var clone = original;
        MergeHelper.MergeDictionaries<TestDataNoMerge>(null, clone);// original.CloneUnder(null, forceCopy);
        // Verify that it's a real copy, or the original object
        Assert.Equal(forceCopy, clone != original);
        // Verify that the content is the same
        AssertSameAs(original, clone);
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
    public void MixingHalves() => MixExpectsDataSimple(DataSimpleHalf1, DataSimpleHalf2);

    [Fact]
    public void MixingPartsWhichOverlapWithIdenticalData() => MixExpectsDataSimple(DataSimpleHalf1, DataSimpleHalf2);

    private static void MixExpectsDataSimple(Dictionary<string, TestDataNoMerge> h1, Dictionary<string, TestDataNoMerge> priority)
    {
        //var clone = h1.CloneUnder(priority);
        MergeHelper.MergeDictionaries(h1, priority); // h1.CloneUnder(priority);
        var expected = DataSimple;
        //AssertSameAs(expected, clone);
        AssertSameAs(expected, h1);
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