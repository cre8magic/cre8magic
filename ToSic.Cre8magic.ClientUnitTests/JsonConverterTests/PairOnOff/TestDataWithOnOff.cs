using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal.Json;

namespace ToSic.Cre8magic.ClientUnitTests.JsonConverterTests.PairOnOff;

public class TestDataWithOnOff
{
    //[JsonConverter(typeof(PairOnOffJsonConverter))]
    public MagicSettingOnOff? Setting { get; set; }
}