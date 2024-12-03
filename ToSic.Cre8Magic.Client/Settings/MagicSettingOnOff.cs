using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Special Setting containing a value to be used when a state is on or off.
/// Typically used to specify strings to place in classes when something:
/// * is active
/// * is home
/// * etc.
/// </summary>
[JsonConverter(typeof(PairOnOffJsonConverter))]
public class MagicSettingOnOff: ICanClone<MagicSettingOnOff>
{
    /// <summary>
    /// Empty constructor for JSON serialization
    /// </summary>
    [PrivateApi]
    public MagicSettingOnOff() {}

    public MagicSettingOnOff(string? on, string? off = null)
    {
        On = on;
        Off = off;
    }

    /// <summary>
    /// Value / Class to add if the setting is on
    /// </summary>
    public string? On { get; init; }

    /// <summary>
    /// Value / Class to add if the setting is off
    /// </summary>
    public string? Off { get; init; }

    MagicSettingOnOff ICanClone<MagicSettingOnOff>.CloneUnder(MagicSettingOnOff? priority, bool forceCopy) =>
        new()
        {
            On = priority?.On ?? On,
            Off = priority?.Off ?? Off
        };

    /// <summary>
    /// Special class just for deserialization, which does not have the JsonConverter attribute.
    /// </summary>
    internal class NoConverterForJsonRead: MagicSettingOnOff;

    internal class NoConverterForJsonWrite(MagicSettingOnOff original) : MagicSettingOnOff(original?.On, original?.Off);
}