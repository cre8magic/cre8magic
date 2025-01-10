using ToSic.Cre8magic.Settings.Values;

namespace ToSic.Cre8magic.ClientUnitTests.JsonConverterTests.PairOnOff;

public class TestDataWithOnOff
{
    //[JsonConverter(typeof(PairOnOffJsonConverter))]
    public MagicSettingOnOff? Setting { get; set; }
}