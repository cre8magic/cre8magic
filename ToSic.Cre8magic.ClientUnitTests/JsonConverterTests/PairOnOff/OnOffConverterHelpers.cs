using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Settings;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.ClientUnitTests.JsonConverterTests.PairOnOff;

internal static class OnOffConverterHelpers
{
    internal static JsonSerializerOptions WithOnOffConverter()
    {
        var sp = SetupServices.Start().AddStandardLogging().Finish();
        //var logger = sp.GetRequiredService<ILogger<JsonConverterBase<MagicSettingOnOff>>>();
        return new(JsonSerializerOptions.Web)
        {
            Converters = { new Settings.Internal.Json.SettingOnOffJsonConverter() /*.GetNew(logger)*/ }
        };
    }


    //internal static JsonSerializerOptions WithContracts()
    //{
    //    static void SetNumberHandlingModifier(JsonTypeInfo jsonTypeInfo)
    //    {
    //        if (jsonTypeInfo.Type == typeof(MagicSettingOnOff))
    //        {
    //            jsonTypeInfo.PolymorphismOptions = new JsonTypeInfo.PolymorphismOptions
    //            {
    //                AsProperty = true,
    //                PropertyName = "type"
    //            };
    //            jsonTypeInfo.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    //        }
    //    }
    //}

}