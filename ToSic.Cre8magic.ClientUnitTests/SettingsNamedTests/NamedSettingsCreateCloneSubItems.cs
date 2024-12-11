using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

public class NamedSettingsCreateCloneSubItems
{

    private static void MixExpects<T>(Dictionary<string, T> foundation, Dictionary<string, T> priority, Dictionary<string, T> expected) where T: TestDataNoMerge, ICanClone<T>
    {
        var result = MergeHelper.CloneMergeDictionaries(priority, foundation);
        AssertSameAs(expected, result);
    }

    private static void AssertSameAs<T>(Dictionary<string, T> expected, Dictionary<string, T> clone) where T : TestDataNoMerge, ICanClone<T>
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
    public void MixEmptyAndName() =>
        MixExpects(DicWithEmpty<TestDataNoMerge>(), DicWithName<TestDataNoMerge>(), DicWithName<TestDataNoMerge>());

    [Fact]
    public void MixNameAndName() =>
        MixExpects(DicWithName<TestDataNoMerge>(), DicWithName<TestDataNoMerge>(), DicWithName<TestDataNoMerge>());

    [Fact]
    public void MixEmptyUnderId() =>
        MixExpects(DicWithEmpty<TestDataNoMerge>(), DicWithId<TestDataNoMerge>(), DicWithId<TestDataNoMerge>());

    [Fact]
    public void MixIdUnderEmpty() =>
        MixExpects(DicWithId<TestDataNoMerge>(), DicWithEmpty<TestDataNoMerge>(), DicWithEmpty<TestDataNoMerge>());

    /// <summary>
    /// This test will take a type which doesn't know about cloning, and will merge.
    /// Result will just be the new (second) item, as they can't be mixed
    /// </summary>
    [Fact]
    public void MixNameAndIdNoClone() => MixExpects(
        DicWithName<TestDataNoMerge>(),
        DicWithId<TestDataNoMerge>(),
        DicWithId<TestDataNoMerge>()
    );

    /// <summary>
    /// This one will take a type which can clone, and will merge.
    /// </summary>
    [Fact]
    public void MixNameAndIdClone() => MixExpects(
        DicWithName<TestDataAbleToMerge>(),
        DicWithId<TestDataAbleToMerge>(),
        DicWithNameAndId<TestDataAbleToMerge>()
    );

    private static Dictionary<string, T> DicWithEmpty<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() },
        };

    private static Dictionary<string, T> DicWithName<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Name = "hello" } },
        };

    private static Dictionary<string, T> DicWithId<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Id = 123 } },
        };

    private static Dictionary<string, T> DicWithNameAndId<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Name = "hello", Id = 123 } },
        };
}