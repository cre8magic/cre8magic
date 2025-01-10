using System.Text.Json;
using ToSic.Cre8magic.Spells.Values;
using static ToSic.Cre8magic.ClientUnitTests.JsonConverterTests.PairOnOff.OnOffConverterHelpers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ToSic.Cre8magic.ClientUnitTests.JsonConverterTests.PairOnOff;

public class SettingOnOffJsonConverter
{

    [Fact]
    public void TestObjectStandardOptions()
    {
        var json = "{\"on\":\"on-value\",\"off\":\"off-value\"}";
        var x = JsonSerializer.Deserialize<MagicSettingOnOff>(json, JsonSerializerOptions.Web);
        Assert.NotNull(x);
        Assert.Equal("on-value", x.On);
        Assert.Equal("off-value", x.Off);
    }

    [Fact]
    public void TestObjectCustomOptions()
    {
        var json = "{\"on\":\"on-value\",\"off\":\"off-value\"}";
        var x = JsonSerializer.Deserialize<MagicSettingOnOff>(json, WithOnOffConverter());
        Assert.NotNull(x);
        Assert.Equal("on-value", x.On);
        Assert.Equal("off-value", x.Off);
    }

    [Fact]
    public void TestArray()
    {
        var json = "[\"on-value\", \"off-value\"]";
        var x = JsonSerializer.Deserialize<MagicSettingOnOff>(json, WithOnOffConverter());
        Assert.NotNull(x);
        Assert.Equal("on-value", x.On);
        Assert.Equal("off-value", x.Off);
    }

    [Fact]
    public void TestString()
    {
        var json = "\"on-value\"";
        var x = JsonSerializer.Deserialize<MagicSettingOnOff>(json, WithOnOffConverter());
        Assert.NotNull(x);
        Assert.Equal("on-value", x.On);
        Assert.Null(x.Off);
    }
}