using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

public class NamedSettingsCreateCloneSubItems
{

    private static void MixExpects<T>(NamedSettings<T> h1, NamedSettings<T> h2, NamedSettings<T> expected) where T: DataForTest, ICanClone<T>
    {
        var clone = h1.CloneWith(h2);
        AssertSameAs(expected, clone);
    }

    private static void AssertSameAs<T>(NamedSettings<T> expected, NamedSettings<T> clone) where T : DataForTest, ICanClone<T>
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
    public void MixEmptyAndName() => MixExpects(GetNoCloneEmpty<DataForTest>(), GetNoCloneWithName<DataForTest>(), GetNoCloneWithName<DataForTest>());
    [Fact]
    public void MixNameAndName() => MixExpects(GetNoCloneWithName<DataForTest>(), GetNoCloneWithName<DataForTest>(), GetNoCloneWithName<DataForTest>());

    [Fact]
    public void MixEmptyAndId() => MixExpects(GetNoCloneEmpty<DataForTest>(), GetWithId<DataForTest>(), GetWithId<DataForTest>());

    /// <summary>
    /// This test will take a type which doesn't know about cloning, and will merge.
    /// Result will just be the new (second) item, as they can't be mixed
    /// </summary>
    [Fact]
    public void MixNameAndIdNoClone() => MixExpects(
        GetNoCloneWithName<DataForTest>(),
        GetWithId<DataForTest>(),
        GetWithId<DataForTest>()
    );

    /// <summary>
    /// This one will take a type which can clone, and will merge.
    /// </summary>
    [Fact]
    public void MixNameAndIdClone() => MixExpects(
        GetNoCloneWithName<DataForTestCanClone>(),
        GetWithId<DataForTestCanClone>(),
        GetWithNameAndId<DataForTestCanClone>()
    );

    private static NamedSettings<T> GetNoCloneEmpty<T>() where T: DataForTest, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() },
        };

    private static NamedSettings<T> GetNoCloneWithName<T>() where T: DataForTest, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Name = "hello" } },
        };

    private static NamedSettings<T> GetWithId<T>() where T: DataForTest, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Id = 123 } },
        };

    private static NamedSettings<T> GetWithNameAndId<T>() where T: DataForTest, ICanClone<T>, new() =>
        new()
        {
            { "subitem", new() { Name = "hello", Id = 123 } },
        };
}