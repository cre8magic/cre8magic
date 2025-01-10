using System.Text.Json;
using ToSic.Cre8magic.Settings.Values;

namespace ToSic.Cre8magic.ClientUnitTests.JsonConverterTests.PairOnOff;

public class SettingOnOffOnObjectWithDecorator
{
    [Fact]
    public void TestObjectStandardOptions()
    {
        var json = "{\"Setting\":{\"on\":\"on-value\",\"off\":\"off-value\"}}";
        var x = JsonSerializer.Deserialize<TestDataWithOnOff>(json, JsonSerializerOptions.Web);
        Assert.NotNull(x);
        Assert.NotNull(x.Setting);
        Assert.Equal("on-value", x.Setting.On);
        Assert.Equal("off-value", x.Setting.Off);
    }

    [Fact]
    public void TestArray()
    {
        var json = "{\"Setting\":[\"on-value\", \"off-value\"]}";
        var x = JsonSerializer.Deserialize<TestDataWithOnOff>(json, JsonSerializerOptions.Web);
        Assert.NotNull(x);
        Assert.NotNull(x.Setting);
        Assert.Equal("on-value", x.Setting.On);
        Assert.Equal("off-value", x.Setting.Off);
    }

    [Fact]
    public void TestString()
    {
        var json = "{\"Setting\":\"on-value\"}";
        var x = JsonSerializer.Deserialize<TestDataWithOnOff>(json, JsonSerializerOptions.Web);
        Assert.NotNull(x);
        Assert.NotNull(x.Setting);
        Assert.Equal("on-value", x.Setting.On);
        Assert.Null(x.Setting.Off);
    }

    [Fact]
    public void Serialize()
    {
        var onOff = new MagicSettingOnOff("on-value", "off-value");
        var json = JsonSerializer.Serialize(onOff, JsonSerializerOptions.Web);
        Assert.Equal("{\"on\":\"on-value\",\"off\":\"off-value\"}", json);
    }
}