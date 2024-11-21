using System.Text.Json;

namespace ToSic.Cre8magic.ClientUnitTests.JsonConverterTests;

public class CustomJsonDictionaryDeserializer
{
    private static DataWithDictionaries GetDataWithDictionaries() => 
        JsonSerializer.Deserialize<DataWithDictionaries>(DataWithDictionaries.Json)!;

    [Fact]
    public void DeserializeVerifyBasics()
    {
        var result = GetDataWithDictionaries();
        Assert.NotNull(result);
        Assert.NotNull(result.Classic);
        Assert.Equal(2, result.Classic.Count);
        Assert.Equal("Value1", result.Classic["Key1"]);
        Assert.Equal("Value2", result.Classic["Key2"]);

        Assert.NotNull(result.Invariant);
        Assert.Equal(2, result.Invariant.Count);
        Assert.Equal("Value1", result.Invariant["Key1"]);
        Assert.Equal("Value2", result.Invariant["Key2"]);
    }

    [Theory]
    [InlineData("Key1", true)]
    [InlineData("KEY1", false)]
    [InlineData("key1", false)]
    public void NormalCaseSensitive(string key, bool expected)
    {
        var result = GetDataWithDictionaries();
        Assert.Equal(expected, result.Classic!.ContainsKey(key));
    }

    [Theory]
    [InlineData("Key1", true)]
    [InlineData("KEY1", true)]
    [InlineData("key1", true)]
    [InlineData("verify-test-works", false)]
    public void SpecialCaseInsensitive(string key, bool expected)
    {
        var result = GetDataWithDictionaries();
        Assert.Equal(expected, result.Invariant!.ContainsKey(key));
    }

    [Fact]
    public void ClassicNull() =>
        Assert.Null(GetDataWithDictionaries().ClassicShouldBeNull);

    [Fact]
    public void ClassicNotNull() =>
        Assert.NotNull(GetDataWithDictionaries().ClassicShouldBeNonNull);

    [Fact]
    public void InvariantNull() =>
        Assert.Null(GetDataWithDictionaries().InvariantShouldBeNull);

    [Fact]
    public void InvariantNotNull() =>
        Assert.NotNull(GetDataWithDictionaries().InvariantShouldBeNonNull);

}