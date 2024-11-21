using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

public class NamedSettingsCreateCloneSubItems
{

    private static void MixExpects<T>(NamedSettings<T> foundation, NamedSettings<T> priority, NamedSettings<T> expected) where T: TestDataNoMerge, ICanClone<T>
    {
        var clone = foundation.CloneUnder(priority);
        AssertSameAs(expected, clone);
    }

    private static void AssertSameAs<T>(NamedSettings<T> expected, NamedSettings<T> clone) where T : TestDataNoMerge, ICanClone<T>
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

    private static NamedSettings<T> DicWithEmpty<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() },
        };

    private static NamedSettings<T> DicWithName<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Name = "hello" } },
        };

    private static NamedSettings<T> DicWithId<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Id = 123 } },
        };

    private static NamedSettings<T> DicWithNameAndId<T>() where T: TestDataNoMerge, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Name = "hello", Id = 123 } },
        };
}